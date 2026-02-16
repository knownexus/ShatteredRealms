using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using ShatteredRealms.Application.DTOs.Auth;
using Xunit;

namespace ShatteredRealms.API.Tests.Integration;

public class AuthIntegrationTests : IntegrationTestBase
{
    public AuthIntegrationTests(IntegrationTestFactory factory) : base(factory) { }

    // -------------------------------------------------------------------------
    // Register
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Register_WithValidData_ShouldReturnTokens()
    {
        // Arrange
        var client  = CreateClient();
        var request = new RegisterRequest
        {
            Email     = $"newuser_{System.Guid.NewGuid()}@example.com", // unique per run
            Password  = "Password123!",
            FirstName = "New",
            LastName  = "User"
        };

        // Act
        var response = await client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var auth = await response.Content.ReadFromJsonAsync<AuthResponse>();
        auth.Should().NotBeNull();
        auth!.AccessToken.Should().NotBeNullOrEmpty();
        auth.RefreshToken.Should().NotBeNullOrEmpty();
        auth.User.Email.Should().Be(request.Email);
    }

    [Fact]
    public async Task Register_WithDuplicateEmail_ShouldReturnConflict()
    {
        // Arrange - register once, then try again with same email
        var client  = CreateClient();
        var request = new RegisterRequest
        {
            Email     = $"duplicate_{System.Guid.NewGuid()}@example.com",
            Password  = "Password123!",
            FirstName = "First",
            LastName  = "User"
        };

        await client.PostAsJsonAsync("/api/auth/register", request);

        // Act
        var response = await client.PostAsJsonAsync("/api/auth/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    // -------------------------------------------------------------------------
    // Login
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Login_WithValidCredentials_ShouldReturnTokens()
    {
        // Arrange
        var client = CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email    = "admin@shatteredrealms.com",
            Password = "TestAdmin@123!"
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var auth = await response.Content.ReadFromJsonAsync<AuthResponse>();
        auth.Should().NotBeNull();
        auth!.AccessToken.Should().NotBeNullOrEmpty();
        auth.RefreshToken.Should().NotBeNullOrEmpty();
        auth.User.Email.Should().Be("admin@shatteredrealms.com");
        auth.User.Roles.Should().Contain("Admin");
    }

    [Fact]
    public async Task Login_WithWrongPassword_ShouldReturnUnauthorized()
    {
        var client   = CreateClient();
        var response = await client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email    = "admin@shatteredrealms.com",
            Password = "WrongPassword"
        });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_WithNonExistentEmail_ShouldReturnUnauthorized()
    {
        var client   = CreateClient();
        var response = await client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email    = "nobody@example.com",
            Password = "Password123!"
        });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // -------------------------------------------------------------------------
    // Protected endpoints - no token
    // -------------------------------------------------------------------------

    [Fact]
    public async Task AccessProtectedEndpoint_WithoutToken_ShouldReturnUnauthorized()
    {
        var client   = CreateClient(); // no auth headers
        var response = await client.GetAsync("/api/users");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // -------------------------------------------------------------------------
    // Protected endpoints - with token
    // -------------------------------------------------------------------------

    [Fact]
    public async Task AccessProtectedEndpoint_WithValidToken_ShouldReturnOk()
    {
        var client   = await CreateAuthenticatedClientAsync();
        var response = await client.GetAsync("/api/users");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // -------------------------------------------------------------------------
    // Refresh token
    // -------------------------------------------------------------------------

    [Fact]
    public async Task RefreshToken_WithValidToken_ShouldReturnNewTokens()
    {
        // Arrange - log in first to get a refresh token
        var client      = CreateClient();
        var loginResp   = await client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email    = "admin@shatteredrealms.com",
            Password = "TestAdmin@123!"
        });
        var auth = await loginResp.Content.ReadFromJsonAsync<AuthResponse>();

        // Act
        var response = await client.PostAsJsonAsync("/api/auth/refresh-token", new RefreshTokenRequest
        {
            RefreshToken = auth!.RefreshToken
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var newAuth = await response.Content.ReadFromJsonAsync<AuthResponse>();
        newAuth.Should().NotBeNull();
        newAuth!.AccessToken.Should().NotBeNullOrEmpty();
        newAuth.RefreshToken.Should().NotBeNullOrEmpty();
        // Tokens should actually be new
        newAuth.AccessToken.Should().NotBe(auth.AccessToken);
        newAuth.RefreshToken.Should().NotBe(auth.RefreshToken);
    }

    [Fact]
    public async Task RefreshToken_WithInvalidToken_ShouldReturnUnauthorized()
    {
        var client   = CreateClient();
        var response = await client.PostAsJsonAsync("/api/auth/refresh-token", new RefreshTokenRequest
        {
            RefreshToken = "this-is-not-a-real-token"
        });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task RefreshToken_AfterRevoke_ShouldReturnUnauthorized()
    {
        // Arrange - log in, then revoke the refresh token
        var client    = await CreateAuthenticatedClientAsync();
        var loginResp = await client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email    = "admin@shatteredrealms.com",
            Password = "Admin@123!"
        });
        var auth = await loginResp.Content.ReadFromJsonAsync<AuthResponse>();

        await client.PostAsJsonAsync("/api/auth/revoke-token", new RefreshTokenRequest
        {
            RefreshToken = auth!.RefreshToken
        });

        // Act - try to use the revoked refresh token
        var response = await client.PostAsJsonAsync("/api/auth/refresh-token", new RefreshTokenRequest
        {
            RefreshToken = auth.RefreshToken
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // -------------------------------------------------------------------------
    // Full flow
    // -------------------------------------------------------------------------

    [Fact]
    public async Task CompleteAuthFlow_RegisterLoginRefreshRevoke_ShouldAllSucceed()
    {
        var client = CreateClient();
        var email  = $"flow_{System.Guid.NewGuid()}@example.com";

        // 1. Register
        var registerResp = await client.PostAsJsonAsync("/api/auth/register", new RegisterRequest
        {
            Email     = email,
            Password  = "Password123!",
            FirstName = "Flow",
            LastName  = "Test"
        });
        registerResp.StatusCode.Should().Be(HttpStatusCode.OK);

        // 2. Login
        var loginResp = await client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email    = email,
            Password = "Password123!"
        });
        loginResp.StatusCode.Should().Be(HttpStatusCode.OK);
        var auth = await loginResp.Content.ReadFromJsonAsync<AuthResponse>();

        // 3. Access protected resource
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth!.AccessToken);
        var protectedResp = await client.GetAsync("/api/users");
        // Note: may get 403 Forbidden here if the new user lacks Users.View permission
        // - this tests that auth itself works, not authorisation
        protectedResp.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Forbidden);

        // 4. Refresh
        var refreshResp = await client.PostAsJsonAsync("/api/auth/refresh-token", new RefreshTokenRequest
        {
            RefreshToken = auth.RefreshToken
        });
        refreshResp.StatusCode.Should().Be(HttpStatusCode.OK);
        var newAuth = await refreshResp.Content.ReadFromJsonAsync<AuthResponse>();

        // 5. Revoke
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newAuth!.AccessToken);
        var revokeResp = await client.PostAsJsonAsync("/api/auth/revoke-token", new RefreshTokenRequest
        {
            RefreshToken = newAuth.RefreshToken
        });
        revokeResp.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // 6. Confirm revoked token no longer works
        var replayResp = await client.PostAsJsonAsync("/api/auth/refresh-token", new RefreshTokenRequest
        {
            RefreshToken = newAuth.RefreshToken
        });
        replayResp.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}