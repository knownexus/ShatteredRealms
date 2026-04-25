using System.Net.Http.Json;
using Blazored.LocalStorage;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Web.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthStateService _authStateService;
    private readonly AuthMemoryCache _cache;

    private bool _clientLoaded = false;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage,
        AuthMemoryCache cache, AuthStateService authStateService)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authStateService = authStateService;
        _cache = cache;
    }

    /// <summary>Called once on client initialization (after prerendering).</summary>
    public async Task InitializeAsync()
    {
        _clientLoaded = true;

        try
        {
            var token = await _localStorage.GetItemAsync<string>("accessToken");
            if (!string.IsNullOrEmpty(token) && IsTokenExpired(token))
            {
                await _localStorage.RemoveItemAsync("accessToken");
                await _localStorage.RemoveItemAsync("refreshToken");
                await _localStorage.RemoveItemAsync("user");
                token = null;
            }
            _cache.AccessToken = token;
        }
        catch
        {
            _cache.AccessToken = null;
        }

        _authStateService.NotifyAuthStateChanged();
    }

    public async Task<(bool success, bool requiresEmailConfirmation, string? errorCode, string? errorMessage)> RegisterAsync(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<RegisterResponse>();
            return (true, data?.RequiresEmailConfirmation ?? false, null, null);
        }

        try
        {
            var json = await response.Content.ReadFromJsonAsync<System.Text.Json.JsonElement>();
            var title  = json.TryGetProperty("title",  out var t) ? t.GetString() : null;
            var detail = json.TryGetProperty("detail", out var d) ? d.GetString() : null;
            return (false, false, title, detail ?? "Registration failed. Please try again.");
        }
        catch
        {
            return (false, false, null, "Registration failed. Please try again.");
        }
    }

    public async Task<(bool success, string? errorMessage)> ConfirmEmailAsync(string userId, string token)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/confirm-email",
            new { userId, token });

        if (response.IsSuccessStatusCode)
        {
            return (true, null);
        }

        try
        {
            var json = await response.Content.ReadFromJsonAsync<System.Text.Json.JsonElement>();
            var detail = json.TryGetProperty("detail", out var d) ? d.GetString() : null;
            return (false, detail ?? "Email confirmation failed. The link may have expired.");
        }
        catch
        {
            return (false, "Email confirmation failed. The link may have expired.");
        }
    }

    public async Task ResendConfirmationAsync(string email)
    {
        await _httpClient.PostAsJsonAsync("api/auth/resend-confirmation", new { email });
    }

    public async Task<(AuthResponse? response, string? errorTitle, string? errorDetail)> LoginAsync(LoginRequest request)
    {
        var httpResponse = await _httpClient.PostAsJsonAsync("api/auth/login", request);
        if (!httpResponse.IsSuccessStatusCode)
        {
            try
            {
                var json = await httpResponse.Content.ReadFromJsonAsync<System.Text.Json.JsonElement>();
                var title  = json.TryGetProperty("title",  out var t) ? t.GetString() : null;
                var detail = json.TryGetProperty("detail", out var d) ? d.GetString() : null;
                return (null, title, detail);
            }
            catch
            {
                return (null, null, "Sign in failed. Please try again.");
            }
        }

        var authResponse = await httpResponse.Content.ReadFromJsonAsync<AuthResponse>();
        if (authResponse == null)
        {
            return (null, null, "Sign in failed. Please try again.");
        }

        _cache.AccessToken = authResponse.AccessToken;

        if (_clientLoaded)
        {
            await _localStorage.SetItemAsync("accessToken",  authResponse.AccessToken);
            await _localStorage.SetItemAsync("refreshToken", authResponse.RefreshToken);
            await _localStorage.SetItemAsync("user",         authResponse.User);
        }

        _authStateService.NotifyAuthStateChanged();
        return (authResponse, null, null);
    }

    public async Task LogoutAsync()
    {
        _cache.AccessToken = null;

        if (_clientLoaded)
        {
            await _localStorage.RemoveItemAsync("accessToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            await _localStorage.RemoveItemAsync("user");
        }

        _authStateService.NotifyAuthStateChanged();
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        if (!string.IsNullOrEmpty(_cache.AccessToken))
        {
            if (IsTokenExpired(_cache.AccessToken))
            {
                _cache.AccessToken = null;
                if (_clientLoaded)
                {
                    await _localStorage.RemoveItemAsync("accessToken");
                    await _localStorage.RemoveItemAsync("refreshToken");
                    await _localStorage.RemoveItemAsync("user");
                }
                return false;
            }
            return true;
        }

        if (!_clientLoaded)
        {
            return false;
        }

        var token = await _localStorage.GetItemAsync<string>("accessToken");
        if (!string.IsNullOrEmpty(token) && IsTokenExpired(token))
        {
            await _localStorage.RemoveItemAsync("accessToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            await _localStorage.RemoveItemAsync("user");
            token = null;
        }
        _cache.AccessToken = token;
        return !string.IsNullOrEmpty(token);
    }

    private static bool IsTokenExpired(string token)
    {
        try
        {
            var parts = token.Split('.');
            if (parts.Length != 3) return true;

            var payload = parts[1];
            // base64url → base64
            payload = payload.Replace('-', '+').Replace('_', '/');
            switch (payload.Length % 4)
            {
                case 2: payload += "=="; break;
                case 3: payload += "=";  break;
            }

            var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(payload));
            using var doc = System.Text.Json.JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("exp", out var expProp)) return true;

            var exp = expProp.GetInt64();
            var expiry = DateTimeOffset.FromUnixTimeSeconds(exp);
            return expiry <= DateTimeOffset.UtcNow;
        }
        catch
        {
            return true;
        }
    }

    public async Task<UserDto?> GetCurrentUserAsync()
    {
        if (!_clientLoaded)
        {
            return null;
        }

        try
        {
            return await _localStorage.GetItemAsync<UserDto>("user");
        }
        catch
        {
            return null;
        }
    }

    /// <summary>Returns the current user's ID, or null if not authenticated.</summary>
    public async Task<string?> GetUserIdAsync()
    {
        var user = await GetCurrentUserAsync();
        return user?.Id;
    }

    /// <summary>
    /// Returns true if the current user's JWT contains the given permission claim.
    /// Permissions are stored in UserDto.Permissions (populated at login from the JWT).
    /// </summary>
    public async Task<bool> HasPermissionAsync(string permission)
    {
        var user = await GetCurrentUserAsync();
        return user?.Permissions.Contains(permission) == true;
    }

    /// <summary>
    /// Returns true if the current user has the Admin or System role.
    /// Used to decide which delete endpoint to call (admin bypass vs author-only).
    /// </summary>
    public async Task<bool> IsAdminAsync()
    {
        var user = await GetCurrentUserAsync();
        if (user == null)
        {
            return false;
        }

        return user.Roles.Contains(Claims.Roles.AdminName)
            || user.Roles.Contains(Claims.Roles.SystemName);
    }
}
