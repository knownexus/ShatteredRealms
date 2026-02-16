using System;
using FluentAssertions;
using ShatteredRealms.Domain.Entities;
using Xunit;

namespace ShatteredRealms.Domain.Tests.Entities;

public class RefreshTokenTests
{
    [Fact]
    public void IsExpired_ShouldReturnTrue_WhenTokenIsExpired()
    {
        // Arrange
        var token = new RefreshToken
        {
            ExpiresAt = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = token.IsExpired;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsExpired_ShouldReturnFalse_WhenTokenIsNotExpired()
    {
        // Arrange
        var token = new RefreshToken
        {
            ExpiresAt = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = token.IsExpired;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsRevoked_ShouldReturnTrue_WhenTokenIsRevoked()
    {
        // Arrange
        var token = new RefreshToken
        {
            RevokedAt = DateTime.UtcNow
        };

        // Act
        var result = token.IsRevoked;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsRevoked_ShouldReturnFalse_WhenTokenIsNotRevoked()
    {
        // Arrange
        var token = new RefreshToken
        {
            RevokedAt = null
        };

        // Act
        var result = token.IsRevoked;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsActive_ShouldReturnTrue_WhenTokenIsNotExpiredAndNotRevoked()
    {
        // Arrange
        var token = new RefreshToken
        {
            ExpiresAt = DateTime.UtcNow.AddDays(1),
            RevokedAt = null
        };

        // Act
        var result = token.IsActive;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsActive_ShouldReturnFalse_WhenTokenIsExpired()
    {
        // Arrange
        var token = new RefreshToken
        {
            ExpiresAt = DateTime.UtcNow.AddDays(-1),
            RevokedAt = null
        };

        // Act
        var result = token.IsActive;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsActive_ShouldReturnFalse_WhenTokenIsRevoked()
    {
        // Arrange
        var token = new RefreshToken
        {
            ExpiresAt = DateTime.UtcNow.AddDays(1),
            RevokedAt = DateTime.UtcNow
        };

        // Act
        var result = token.IsActive;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsActive_ShouldReturnFalse_WhenTokenIsExpiredAndRevoked()
    {
        // Arrange
        var token = new RefreshToken
        {
            ExpiresAt = DateTime.UtcNow.AddDays(-1),
            RevokedAt = DateTime.UtcNow
        };

        // Act
        var result = token.IsActive;

        // Assert
        result.Should().BeFalse();
    }
}
