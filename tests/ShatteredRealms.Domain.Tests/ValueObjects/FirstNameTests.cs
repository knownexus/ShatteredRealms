using FluentAssertions;
using ShatteredRealms.Domain.ValueObjects;

namespace ShatteredRealms.Domain.Tests.ValueObjects;

public sealed class FirstNameTests
{
    [Theory]
    [InlineData("Alice")]
    [InlineData("A")]
    [InlineData("Jean-Pierre")]
    public void Create_ReturnsSuccess_ForValidFirstNames(string name)
    {
        var result = FirstName.Create(name);
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(name);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_ReturnsFailure_WhenFirstNameIsEmpty(string name)
    {
        var result = FirstName.Create(name);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Create_ReturnsFailure_WhenFirstNameExceedsMaxLength()
    {
        var result = FirstName.Create(new string('A', 51));
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void TwoFirstNamesWithSameValue_AreEqual()
    {
        var a = FirstName.Create("Alice").Value;
        var b = FirstName.Create("Alice").Value;
        a.Should().Be(b);
    }
}
