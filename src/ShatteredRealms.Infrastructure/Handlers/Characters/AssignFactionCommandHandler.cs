using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.ActivityLog;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class AssignFactionCommandHandler : IRequestHandler<AssignFactionCommand, Result<CharacterDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public AssignFactionCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result<CharacterDto>> Handle(AssignFactionCommand request, CancellationToken cancellationToken)
    {
        var character = await _context.Character
            .Include(c => c.Owner)
            .Include(c => c.Position)
            .FirstOrDefaultAsync(c => c.Id == request.CharacterId, cancellationToken);

        if (character is null)
        {
            return Result.Failure<CharacterDto>(DomainErrors.Character.NotFound);
        }

        var oldFaction = character.Faction;
        character.Faction = request.Faction;

        _context.ActivityLog.Add(new ActivityLog
        {
            Id          = Guid.NewGuid(),
            UserId      = request.RequestingUserId,
            CharacterId = character.Id,
            Description = $"Assigned faction '{request.Faction}' to character '{character.Name}' (was: '{oldFaction}')",
            Date        = DateTime.UtcNow,
        });

        await _context.SaveChangesAsync(cancellationToken);

        await _analytics.TrackAsync(TelemetryActionType.CharacterFactionAssigned, request.RequestingUserId, string.Empty,
            targetId: character.Id.ToString(), targetName: character.Name,
            details: $"Faction: {request.Faction} (was: {oldFaction})", cancellationToken: cancellationToken);

        return Result.Success(CreateCharacterCommandHandler.MapToDto(character, null));
    }
}
