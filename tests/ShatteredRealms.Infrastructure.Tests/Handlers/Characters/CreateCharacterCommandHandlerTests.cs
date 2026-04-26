using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Infrastructure.Data;
using ShatteredRealms.Infrastructure.Handlers.Characters;

namespace ShatteredRealms.Infrastructure.Tests.Handlers.Characters;

public sealed class CreateCharacterCommandHandlerTests
{
    private static ApplicationDbContext CreateContext() =>
        new(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);

    [Fact]
    public async Task Handle_ReturnsCharacterDto_WhenAllFieldsValid()
    {
        await using var ctx = CreateContext();
        var handler = new CreateCharacterCommandHandler(ctx);

        var result = await handler.Handle(
            new CreateCharacterCommand("user-1", "Aldric", "Norman", "Kingdom of England"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be("Aldric");
        result.Value.Nationality.Should().Be("Norman");
        result.Value.Faction.Should().Be("Kingdom of England");
        result.Value.Level.Should().Be(1);
        result.Value.Experience.Should().Be(0);
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenNameIsEmpty()
    {
        await using var ctx = CreateContext();
        var handler = new CreateCharacterCommandHandler(ctx);

        var result = await handler.Handle(
            new CreateCharacterCommand("user-1", "", "Norman", "Kingdom of England"),
            CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Character.NameRequired);
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenNationalityIsEmpty()
    {
        await using var ctx = CreateContext();
        var handler = new CreateCharacterCommandHandler(ctx);

        var result = await handler.Handle(
            new CreateCharacterCommand("user-1", "Aldric", "", "Kingdom of England"),
            CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Character.NationalityRequired);
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenFactionIsEmpty()
    {
        await using var ctx = CreateContext();
        var handler = new CreateCharacterCommandHandler(ctx);

        var result = await handler.Handle(
            new CreateCharacterCommand("user-1", "Aldric", "Norman", ""),
            CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Character.FactionRequired);
    }

    [Fact]
    public async Task Handle_PersistsCharacter_ToDatabase()
    {
        await using var ctx = CreateContext();
        var handler = new CreateCharacterCommandHandler(ctx);

        await handler.Handle(
            new CreateCharacterCommand("user-1", "Aldric", "Norman", "Kingdom of England"),
            CancellationToken.None);

        ctx.Character.Should().HaveCount(1);
        ctx.Character.First().Faction.Should().Be("Kingdom of England");
    }

    [Fact]
    public async Task Handle_WritesActivityLog_OnSuccess()
    {
        await using var ctx = CreateContext();
        var handler = new CreateCharacterCommandHandler(ctx);

        await handler.Handle(
            new CreateCharacterCommand("user-1", "Aldric", "Norman", "Kingdom of England"),
            CancellationToken.None);

        ctx.ActivityLog.Should().HaveCount(1);
        ctx.ActivityLog.First().UserId.Should().Be("user-1");
    }
}
