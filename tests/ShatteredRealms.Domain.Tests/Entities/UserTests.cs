using FluentAssertions;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.User;

namespace ShatteredRealms.Domain.Tests.Entities;

public sealed class UserTests
{
    // ── Defaults ──────────────────────────────────────────────────────────

    [Fact]
    public void User_ShouldInitialiseWithDefaultValues()
    {
        // Act
        var user = new User();

        // Assert
        user.FirstName.Should().Be(string.Empty);
        user.LastName.Should().Be(string.Empty);
        user.UserRoles.Should().NotBeNull().And.BeEmpty();
        user.RefreshTokens.Should().NotBeNull().And.BeEmpty();
        user.UserEmergencyContact.Should().NotBeNull().And.BeEmpty();
    }

    // ── Validate - happy path ─────────────────────────────────────────────

    [Fact]
    public void Validate_ReturnsSuccess_WhenAllFieldsAreValid()
    {
        // Arrange
        var user = new User
        {
            FirstName = "Alice",
            LastName = "Smith",
            Email = "alice@example.com",
        };

        // Act
        var result = User.Validate(user);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    // ── Validate - first name failures ───────────────────────────────────

    [Fact]
    public void Validate_ReturnsFailure_WhenFirstNameIsEmpty()
    {
        // Arrange
        var user = new User { FirstName = "", LastName = "Smith", Email = "alice@example.com" };

        // Act
        var result = User.Validate(user);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Validate_ReturnsFailure_WhenFirstNameExceedsMaxLength()
    {
        // Arrange
        var user = new User
        {
            FirstName = new string('A', 51),
            LastName = "Smith",
            Email = "alice@example.com",
        };

        // Act
        var result = User.Validate(user);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    // ── Validate - last name failures ────────────────────────────────────

    [Fact]
    public void Validate_ReturnsFailure_WhenLastNameIsEmpty()
    {
        // Arrange
        var user = new User { FirstName = "Alice", LastName = "", Email = "alice@example.com" };

        // Act
        var result = User.Validate(user);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    // ── Validate - email failures ─────────────────────────────────────────

    [Fact]
    public void Validate_ReturnsFailure_WhenEmailIsEmpty()
    {
        // Arrange
        var user = new User { FirstName = "Alice", LastName = "Smith", Email = "" };

        // Act
        var result = User.Validate(user);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Validate_ReturnsFailure_WhenEmailHasNoAtSign()
    {
        // Arrange
        var user = new User { FirstName = "Alice", LastName = "Smith", Email = "notanemail" };

        // Act
        var result = User.Validate(user);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Validate_ReturnsFailure_WhenEmailIsNull()
    {
        // Arrange
        var user = new User { FirstName = "Alice", LastName = "Smith", Email = null };

        // Act
        var result = User.Validate(user);

        // Assert
        result.IsFailure.Should().BeTrue();
    }
}
