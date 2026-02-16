using System.Net.Http.Headers;
using System.Net.Http.Json;
using ShatteredRealms.Application.DTOs.Auth;

namespace ShatteredRealms.API.Tests.Integration;

/// <summary>
/// Base class for integration tests.
/// Creates a fresh HttpClient per test so auth headers never bleed between tests.
/// </summary>
public abstract class IntegrationTestBase : IClassFixture<IntegrationTestFactory>
{
    protected readonly IntegrationTestFactory Factory;

    protected IntegrationTestBase(IntegrationTestFactory factory)
    {
        Factory = factory;
    }

    /// <summary>Creates a clean client with no auth headers.</summary>
    protected HttpClient CreateClient() => Factory.CreateClient();

    /// <summary>Creates a client pre-authenticated as the given user.</summary>
    protected async Task<HttpClient> CreateAuthenticatedClientAsync(
        string email = "admin@shatteredrealms.com",
        string password = "TestAdmin@123!")
    {
        var client = CreateClient();

        var response = await client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email = email,
            Password = password,
        });

        response.EnsureSuccessStatusCode();

        var auth = await response.Content.ReadFromJsonAsync<AuthResponse>();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", auth!.AccessToken);

        return client;
    }
}
