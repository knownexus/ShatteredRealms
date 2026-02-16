using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace ShatteredRealms.Web.Services;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthMemoryCache _cache;

    public AuthTokenHandler(ILocalStorageService localStorage, AuthMemoryCache cache)
    {
        _localStorage = localStorage;
        _cache = cache;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var token = _cache.AccessToken;

            // Get token from local storage
            if (string.IsNullOrEmpty(token))
            {
                token = await _localStorage.GetItemAsync<string>("accessToken", cancellationToken);
            }
            Console.WriteLine($"[AuthTokenHandler] Token found: {token != null}");

            // Add token to request headers if it exists
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine($"[AuthTokenHandler] Attaching token to request: {request.Method} {request.RequestUri}");
                Console.WriteLine($"[AuthTokenHandler] Token (first 20 chars): {token.Substring(0, Math.Min(20, token.Length))}...");
            }
            else
            {
                Console.WriteLine($"[AuthTokenHandler] No token found for request: {request.Method} {request.RequestUri}");
            }
        }
        catch (InvalidOperationException ex)
        {
            // LocalStorage not available during prerendering - that's OK
            // The request will just go through without an auth token
            Console.WriteLine($"[AuthTokenHandler] LocalStorage not available (prerendering): {ex.Message}");
        }

        var response = await base.SendAsync(request, cancellationToken);
        Console.WriteLine($"[AuthTokenHandler] Response status for {request.Method} {request.RequestUri}: {(int)response.StatusCode} ({response.StatusCode})");

        return response;
    }
}
