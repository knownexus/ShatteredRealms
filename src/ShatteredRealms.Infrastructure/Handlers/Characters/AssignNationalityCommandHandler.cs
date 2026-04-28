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

public sealed class AssignNationalityCommandHandler : IRequestHandler<AssignNationalityCommand, Result<CharacterDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public AssignNationalityCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result<CharacterDto>> Handle(AssignNationalityCommand request, CancellationToken cancellationToken)
    {
        var character = await _context.Character
            .Include(c => c.Owner)
            .Include(c => c.Position)
            .FirstOrDefaultAsync(c => c.Id == request.CharacterId, cancellationToken);

        if (character is null)
        {
            return Result.Failure<CharacterDto>(DomainErrors.Character.NotFound);
        }

        var oldNationality = character.Nationality;
        character.Nationality = request.Nationality;

        _context.ActivityLog.Add(new ActivityLog
        {
            Id          = Guid.NewGuid(),
            UserId      = request.RequestingUserId,
            CharacterId = character.Id,
            Description = $"Assigned nationality '{request.Nationality}' to character '{character.Name}' (was: '{oldNationality}')",
            Date        = DateTime.UtcNow,
        });

        await _context.SaveChangesAsync(cancellationToken);

        await _analytics.TrackAsync(TelemetryActionType.CharacterNationalityAssigned, request.RequestingUserId, string.Empty,
            targetId: character.Id.ToString(), targetName: character.Name,
            details: $"Nationality: {request.Nationality} (was: {oldNationality})", cancellationToken: cancellationToken);

        return Result.Success(CreateCharacterCommandHandler.MapToDto(character, null));
    }
}
