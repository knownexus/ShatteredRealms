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

public sealed class AssignExperienceCommandHandlerTests
{
    private static ApplicationDbContext CreateContext() =>
        new(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);

    private static void SeedUser(ApplicationDbContext ctx, string userId = "user-1")
    {
        ctx.Users.Add(new User
        {
            Id                 = userId,
            UserName           = $"{userId}@test.com",
            NormalizedUserName = $"{userId}@test.com".ToUpper(),
            Email              = $"{userId}@test.com",
            NormalizedEmail    = $"{userId}@test.com".ToUpper(),
            ConcurrencyStamp   = Guid.NewGuid().ToString(),
            SecurityStamp      = Guid.NewGuid().ToString(),
        });
        ctx.SaveChanges();
    }

    private static Character SeedCharacter(ApplicationDbContext ctx, int experience = 50)
    {
        SeedUser(ctx);
        var character = new Character
        {
            UserId      = "user-1", Name = "Aldric",
            Nationality = "Norman", Faction = "Kingdom of England",
            Level       = 1, Experience = experience, CreatedAt = DateTime.UtcNow
        };
        ctx.Character.Add(character);
        ctx.SaveChanges();
        return character;
    }

    [Fact]
    public async Task Handle_AddsXp_ToCharacter()
    {
        await using var ctx = CreateContext();
        var character = SeedCharacter(ctx, experience: 50);
        var handler = new AssignExperienceCommandHandler(ctx);

        var result = await handler.Handle(
            new AssignExperienceCommand(character.Id, 100, "em-1"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Experience.Should().Be(150);
    }

    [Fact]
    public async Task Handle_FloorsExperienceAtZero_WhenSubtractionWouldGoNegative()
    {
        await using var ctx = CreateContext();
        var character = SeedCharacter(ctx, experience: 10);
        var handler = new AssignExperienceCommandHandler(ctx);

        var result = await handler.Handle(
            new AssignExperienceCommand(character.Id, -100, "em-1"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Experience.Should().Be(0);
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenCharacterNotFound()
    {
        await using var ctx = CreateContext();
        var handler = new AssignExperienceCommandHandler(ctx);

        var result = await handler.Handle(
            new AssignExperienceCommand(999, 50, "em-1"),
            CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Character.NotFound);
    }

    [Fact]
    public async Task Handle_WritesActivityLog_WithNote()
    {
        await using var ctx = CreateContext();
        var character = SeedCharacter(ctx);
        var handler = new AssignExperienceCommandHandler(ctx);

        await handler.Handle(
            new AssignExperienceCommand(character.Id, 50, "em-1", "Attended Spring Event"),
            CancellationToken.None);

        ctx.ActivityLog.Should().HaveCount(1);
        ctx.ActivityLog.First().Description.Should().Contain("Attended Spring Event");
    }

    [Fact]
    public async Task Handle_WritesActivityLog_WhenNoNote()
    {
        await using var ctx = CreateContext();
        var character = SeedCharacter(ctx);
        var handler = new AssignExperienceCommandHandler(ctx);

        await handler.Handle(
            new AssignExperienceCommand(character.Id, 50, "em-1"),
            CancellationToken.None);

        ctx.ActivityLog.Should().HaveCount(1);
    }
}
