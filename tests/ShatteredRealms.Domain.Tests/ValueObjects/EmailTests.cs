using FluentAssertions;
using ShatteredRealms.Domain.ValueObjects;

namespace ShatteredRealms.Domain.Tests.ValueObjects;

public sealed class EmailTests
{
    [Theory]
    [InlineData("user@example.com")]
    [InlineData("user.name+tag@example.co.uk")]
    [InlineData("a@b.com")]
    public void Create_ReturnsSuccess_ForValidEmails(string email)
    {
        var result = Email.Create(email);
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(email);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_ReturnsFailure_WhenEmailIsEmpty(string email)
    {
        var result = Email.Create(email);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Create_ReturnsFailure_WhenEmailHasNoAtSign()
    {
        var result = Email.Create("notanemail");
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Create_ReturnsFailure_WhenEmailExceedsMaxLength()
    {
        var longEmail = new string('a', 250) + "@b.com"; // > 255 chars
        var result = Email.Create(longEmail);
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Create_ReturnsFailure_WhenEmailHasMultipleAtSigns()
    {
        var result = Email.Create("a@b@c.com");
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void TwoEmailsWithSameValue_AreEqual()
    {
        var a = Email.Create("test@example.com").Value;
        var b = Email.Create("test@example.com").Value;
        a.Should().Be(b);
    }

    [Fact]
    public void TwoEmailsWithDifferentValues_AreNotEqual()
    {
        var a = Email.Create("a@example.com").Value;
        var b = Email.Create("b@example.com").Value;
        a.Should().NotBe(b);
    }
}
