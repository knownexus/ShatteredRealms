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
            _cache.AccessToken = token;
        }
        catch
        {
            _cache.AccessToken = null;
        }
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (authResponse == null)
        {
            return null;
        }

        _cache.AccessToken = authResponse.AccessToken;

        if (_clientLoaded)
        {
            await _localStorage.SetItemAsync("accessToken",  authResponse.AccessToken);
            await _localStorage.SetItemAsync("refreshToken", authResponse.RefreshToken);
            await _localStorage.SetItemAsync("user",         authResponse.User);
        }

        _authStateService.NotifyAuthStateChanged();
        return authResponse;
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
            return true;
        }

        if (!_clientLoaded)
        {
            return false;
        }

        var token = await _localStorage.GetItemAsync<string>("accessToken");
        _cache.AccessToken = token;
        return !string.IsNullOrEmpty(token);
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
