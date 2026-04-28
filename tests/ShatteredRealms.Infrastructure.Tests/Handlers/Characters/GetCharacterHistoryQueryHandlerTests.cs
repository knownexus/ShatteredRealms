using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShatteredRealms.Application.Features.Characters.Queries;
using ShatteredRealms.Domain.Entities.ActivityLog;
using ShatteredRealms.Domain.Entities.Character;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Infrastructure.Data;
using ShatteredRealms.Infrastructure.Handlers.Characters;

namespace ShatteredRealms.Infrastructure.Tests.Handlers.Characters;

public sealed class GetCharacterHistoryQueryHandlerTests
{
    private static ApplicationDbContext CreateContext() =>
        new(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);

    private static (Character character, User organiser) SeedData(ApplicationDbContext ctx)
    {
        ctx.Users.Add(new User
        {
            Id = "user-1", UserName = "player@test.com",
            NormalizedUserName = "PLAYER@TEST.COM",
            Email = "player@test.com", NormalizedEmail = "PLAYER@TEST.COM",
            ConcurrencyStamp = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(),
        });
        var organiser = new User
        {
            Id = "em-1", UserName = "organiser@test.com",
            NormalizedUserName = "ORGANISER@TEST.COM",
            Email = "organiser@test.com", NormalizedEmail = "ORGANISER@TEST.COM",
            ConcurrencyStamp = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(),
        };
        ctx.Users.Add(organiser);
        var character = new Character
        {
            UserId = "user-1", Name = "Aldric",
            Nationality = "Norman", Faction = "Kingdom of England",
            Level = 1, Experience = 0, CreatedAt = DateTime.UtcNow
        };
        ctx.Character.Add(character);
        ctx.SaveChanges();

        ctx.ActivityLog.Add(new ActivityLog
        {
            Id = Guid.NewGuid(), UserId = "em-1", CharacterId = character.Id,
            Description = "Assigned 100 XP to character 'Aldric' (new total: 100) - Spring Event",
            Date = DateTime.UtcNow.AddHours(-2)
        });
        ctx.ActivityLog.Add(new ActivityLog
        {
            Id = Guid.NewGuid(), UserId = "em-1", CharacterId = character.Id,
            Description = "Assigned position 'Knight' to character 'Aldric'",
            Date = DateTime.UtcNow.AddHours(-1)
        });
        ctx.ActivityLog.Add(new ActivityLog
        {
            Id = Guid.NewGuid(), UserId = "user-1", CharacterId = null,
            Description = "Some unrelated log entry",
            Date = DateTime.UtcNow
        });
        ctx.SaveChanges();
        return (character, organiser);
    }

    [Fact]
    public async Task Handle_ReturnsHistory_ForCharacter()
    {
        await using var ctx = CreateContext();
        var (character, _) = SeedData(ctx);
        var handler = new GetCharacterHistoryQueryHandler(ctx);

        var result = await handler.Handle(new GetCharacterHistoryQuery(character.Id), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_ReturnsHistory_OrderedNewestFirst()
    {
        await using var ctx = CreateContext();
        var (character, _) = SeedData(ctx);
        var handler = new GetCharacterHistoryQueryHandler(ctx);

        var result = await handler.Handle(new GetCharacterHistoryQuery(character.Id), CancellationToken.None);

        result.Value[0].Description.Should().Contain("Knight");
        result.Value[1].Description.Should().Contain("XP");
    }

    [Fact]
    public async Task Handle_IncludesPerformedByName()
    {
        await using var ctx = CreateContext();
        var (character, organiser) = SeedData(ctx);
        var handler = new GetCharacterHistoryQueryHandler(ctx);

        var result = await handler.Handle(new GetCharacterHistoryQuery(character.Id), CancellationToken.None);

        result.Value[0].PerformedByName.Should().Be(organiser.UserName);
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenCharacterNotFound()
    {
        await using var ctx = CreateContext();
        var handler = new GetCharacterHistoryQueryHandler(ctx);

        var result = await handler.Handle(new GetCharacterHistoryQuery(999), CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Character.NotFound);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoHistory()
    {
        await using var ctx = CreateContext();
        ctx.Users.Add(new User
        {
            Id = "user-1", UserName = "player@test.com",
            NormalizedUserName = "PLAYER@TEST.COM", Email = "player@test.com",
            NormalizedEmail = "PLAYER@TEST.COM", ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString(),
        });
        var character = new Character
        {
            UserId = "user-1", Name = "Aldric", Nationality = "Norman",
            Faction = "Kingdom of England", Level = 1, Experience = 0, CreatedAt = DateTime.UtcNow
        };
        ctx.Character.Add(character);
        ctx.SaveChanges();
        var handler = new GetCharacterHistoryQueryHandler(ctx);

        var result = await handler.Handle(new GetCharacterHistoryQuery(character.Id), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
}
