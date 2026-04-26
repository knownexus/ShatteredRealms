using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Domain.Entities.Character;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Infrastructure.Data;
using ShatteredRealms.Infrastructure.Handlers.Characters;

namespace ShatteredRealms.Infrastructure.Tests.Handlers.Characters;

public sealed class AssignPositionCommandHandlerTests
{
    private static ApplicationDbContext CreateContext() =>
        new(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);

    private static (Character character, Position position) SeedData(ApplicationDbContext ctx)
    {
        ctx.Users.Add(new User
        {
            Id                 = "user-1",
            UserName           = "user1@test.com",
            NormalizedUserName = "USER1@TEST.COM",
            Email              = "user1@test.com",
            NormalizedEmail    = "USER1@TEST.COM",
            ConcurrencyStamp   = Guid.NewGuid().ToString(),
            SecurityStamp      = Guid.NewGuid().ToString(),
        });
        var character = new Character
        {
            UserId      = "user-1", Name = "Aldric",
            Nationality = "Norman", Faction = "Kingdom of England",
            Level       = 1, Experience = 0, CreatedAt = DateTime.UtcNow
        };
        var position = new Position { Id = 100, Name = "Knight", Description = "A mounted warrior" };
        ctx.Character.Add(character);
        ctx.Position.Add(position);
        ctx.SaveChanges();
        return (character, position);
    }

    [Fact]
    public async Task Handle_AssignsPosition_ToCharacter()
    {
        await using var ctx = CreateContext();
        var (character, position) = SeedData(ctx);
        var handler = new AssignPositionCommandHandler(ctx);

        var result = await handler.Handle(
            new AssignPositionCommand(character.Id, position.Id, "em-1"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.PositionId.Should().Be(position.Id);
        result.Value.PositionName.Should().Be("Knight");
    }

    [Fact]
    public async Task Handle_ClearsPosition_WhenPositionIdIsNull()
    {
        await using var ctx = CreateContext();
        var (character, position) = SeedData(ctx);
        character.PositionId = position.Id;
        ctx.SaveChanges();

        var handler = new AssignPositionCommandHandler(ctx);

        var result = await handler.Handle(
            new AssignPositionCommand(character.Id, null, "em-1"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.PositionId.Should().BeNull();
        result.Value.PositionName.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenCharacterNotFound()
    {
        await using var ctx = CreateContext();
        SeedData(ctx);
        var handler = new AssignPositionCommandHandler(ctx);

        var result = await handler.Handle(
            new AssignPositionCommand(999, 100, "em-1"),
            CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Character.NotFound);
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenPositionNotFound()
    {
        await using var ctx = CreateContext();
        var (character, _) = SeedData(ctx);
        var handler = new AssignPositionCommandHandler(ctx);

        var result = await handler.Handle(
            new AssignPositionCommand(character.Id, 999, "em-1"),
            CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Position.NotFound);
    }

    [Fact]
    public async Task Handle_WritesActivityLog_OnSuccess()
    {
        await using var ctx = CreateContext();
        var (character, position) = SeedData(ctx);
        var handler = new AssignPositionCommandHandler(ctx);

        await handler.Handle(
            new AssignPositionCommand(character.Id, position.Id, "em-1"),
            CancellationToken.None);

        ctx.ActivityLog.Should().HaveCount(1);
        ctx.ActivityLog.First().UserId.Should().Be("em-1");
    }
}
