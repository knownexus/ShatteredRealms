using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using ShatteredRealms.Infrastructure.Services;
using Xunit;

namespace ShatteredRealms.Infrastructure.Tests.Services;

public class TokenServiceTests
{
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;

    public TokenServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            {"Jwt:Key", "YourSuperSecretKeyThatIsAtLeast32CharactersLongForHS256!"},
            {"Jwt:Issuer", "TestIssuer"},
            {"Jwt:Audience", "TestAudience"},
            {"Jwt:ExpiresInMinutes", "60"}
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        _tokenService = new TokenService(_configuration);
    }

    [Fact]
    public void GenerateAccessToken_ShouldReturnValidJwtToken()
    {
        // Arrange
        var userId = "user-123";
        var email = "test@example.com";
        var roles = new List<string> { "Admin", "User" };
        var permissions = new List<string> { "Users.Read", "Users.Write" };

        // Act
        var token = _tokenService.GenerateAccessToken(userId, email, roles, permissions);

        // Assert
        token.Should().NotBeNullOrEmpty();

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == userId);
        jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == email);
        jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).Should().BeEquivalentTo(roles);
        jwtToken.Claims.Where(c => c.Type == "Permission").Select(c => c.Value).Should().BeEquivalentTo(permissions);
        jwtToken.Issuer.Should().Be("TestIssuer");
        jwtToken.Audiences.Should().Contain("TestAudience");
    }

    [Fact]
    public void GenerateAccessToken_ShouldSetCorrectExpiration()
    {
        // Arrange
        var userId = "user-123";
        var email = "test@example.com";
        var roles = new List<string>();
        var permissions = new List<string>();

        // Act
        var token = _tokenService.GenerateAccessToken(userId, email, roles, permissions);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var expectedExpiration = DateTime.UtcNow.AddMinutes(60);
        jwtToken.ValidTo.Should().BeCloseTo(expectedExpiration, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void GenerateRefreshToken_ShouldReturnBase64String()
    {
        // Act
        var token = _tokenService.GenerateRefreshToken();

        // Assert
        token.Should().NotBeNullOrEmpty();
        token.Should().MatchRegex("^[A-Za-z0-9+/=]+$"); // Base64 pattern
    }

    [Fact]
    public void GenerateRefreshToken_ShouldReturnDifferentTokensOnMultipleCalls()
    {
        // Act
        var token1 = _tokenService.GenerateRefreshToken();
        var token2 = _tokenService.GenerateRefreshToken();

        // Assert
        token1.Should().NotBe(token2);
    }

    [Fact]
    public void GetPrincipalFromExpiredToken_ShouldReturnClaimsPrincipal_ForValidToken()
    {
        // Arrange
        var userId = "user-123";
        var email = "test@example.com";
        var roles = new List<string> { "Admin" };
        var permissions = new List<string> { "Users.Read" };

        var token = _tokenService.GenerateAccessToken(userId, email, roles, permissions);

        // Act
        var principal = _tokenService.GetPrincipalFromExpiredToken(token);

        // Assert
        principal.Should().NotBeNull();
        principal!.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == userId);
        principal.Claims.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == email);
    }

    [Fact]
    public void GetPrincipalFromExpiredToken_ShouldThrowException_ForInvalidToken()
    {
        // Arrange
        var invalidToken = "invalid.token.string";

        // Act
        Action act = () => _tokenService.GetPrincipalFromExpiredToken(invalidToken);

        // Assert
        act.Should().Throw<Exception>();
    }
}
