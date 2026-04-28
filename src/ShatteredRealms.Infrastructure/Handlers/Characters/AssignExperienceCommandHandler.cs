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

public sealed class AssignExperienceCommandHandler : IRequestHandler<AssignExperienceCommand, Result<CharacterDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public AssignExperienceCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result<CharacterDto>> Handle(AssignExperienceCommand request, CancellationToken cancellationToken)
    {
        var character = await _context.Character
            .Include(c => c.Owner)
            .Include(c => c.Position)
            .FirstOrDefaultAsync(c => c.Id == request.CharacterId, cancellationToken);

        if (character is null)
        {
            return Result.Failure<CharacterDto>(DomainErrors.Character.NotFound);
        }

        character.Experience = Math.Max(0, character.Experience + request.XpToAdd);

        var description = string.IsNullOrWhiteSpace(request.Note)
            ? $"Assigned {request.XpToAdd} XP to character '{character.Name}' (new total: {character.Experience})"
            : $"Assigned {request.XpToAdd} XP to character '{character.Name}' (new total: {character.Experience}) - {request.Note}";

        _context.ActivityLog.Add(new ActivityLog
        {
            Id          = Guid.NewGuid(),
            UserId      = request.RequestingUserId,
            CharacterId = character.Id,
            Description = description,
            Date        = DateTime.UtcNow,
        });

        await _context.SaveChangesAsync(cancellationToken);

        var telemetryDetails = string.IsNullOrWhiteSpace(request.Note)
            ? $"+{request.XpToAdd} XP (total: {character.Experience})"
            : $"+{request.XpToAdd} XP (total: {character.Experience}) - {request.Note}";

        await _analytics.TrackAsync(TelemetryActionType.CharacterXpAssigned, request.RequestingUserId, string.Empty,
            targetId: character.Id.ToString(), targetName: character.Name,
            details: telemetryDetails, cancellationToken: cancellationToken);

        return Result.Success(CreateCharacterCommandHandler.MapToDto(character, null));
    }
}
