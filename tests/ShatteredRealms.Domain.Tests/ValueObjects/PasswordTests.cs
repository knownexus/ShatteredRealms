using FluentAssertions;
using ShatteredRealms.Domain.ValueObjects;

namespace ShatteredRealms.Domain.Tests.ValueObjects;

public sealed class PasswordTests
{
    [Fact]
    public void Create_ReturnsSuccess_WhenPasswordMeetsAllRequirements()
    {
        var result = Password.Create("ValidPass1!");
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_ReturnsFailure_WhenPasswordIsEmpty(string password)
    {
        var result = Password.Create(password);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Create_ReturnsFailure_WhenPasswordIsTooShort()
    {
        var result = Password.Create("Ab1!"); // 4 chars - under RequiredLength of 8
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Create_ReturnsFailure_WhenPasswordHasNoDigit()
    {
        var result = Password.Create("NoDigits!");
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Create_ReturnsFailure_WhenPasswordHasNoUppercase()
    {
        var result = Password.Create("nouppercase1!");
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Create_ReturnsFailure_WhenPasswordHasNoLowercase()
    {
        var result = Password.Create("NOLOWER1!");
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Create_ReturnsFailure_WhenPasswordHasNoSpecialCharacter()
    {
        var result = Password.Create("NoSpecial1");
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Create_ReturnsFailure_WhenPasswordExceedsMaxLength()
    {
        var result = Password.Create(new string('A', 255) + "a1!");
        result.IsFailure.Should().BeTrue();
    }
}
