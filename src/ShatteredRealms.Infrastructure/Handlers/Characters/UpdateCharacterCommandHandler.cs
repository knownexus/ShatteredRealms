using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class UpdateCharacterCommandHandler : IRequestHandler<UpdateCharacterCommand, Result<CharacterDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public UpdateCharacterCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result<CharacterDto>> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result.Failure<CharacterDto>(DomainErrors.Character.NameRequired);
        }

        if (string.IsNullOrWhiteSpace(request.Nationality))
        {
            return Result.Failure<CharacterDto>(DomainErrors.Character.NationalityRequired);
        }

        if (string.IsNullOrWhiteSpace(request.Faction))
        {
            return Result.Failure<CharacterDto>(DomainErrors.Character.FactionRequired);
        }

        var character = await _context.Character
                                      .Include(c => c.Owner)
                                      .Include(c => c.Position)
                                      .FirstOrDefaultAsync(c => c.Id == request.CharacterId, cancellationToken);

        if (character is null)
        {
            return Result.Failure<CharacterDto>(DomainErrors.Character.NotFound);
        }

        character.Name        = request.Name.Trim();
        character.Nationality = request.Nationality.Trim();
        character.Faction     = request.Faction.Trim();
        character.Level       = request.Level;
        character.Experience  = request.Experience;

        await _context.SaveChangesAsync(cancellationToken);

        var actorId = request.ActorId ?? character.UserId;
        await _analytics.TrackAsync(TelemetryActionType.CharacterUpdated, actorId, string.Empty,
            targetId: character.Id.ToString(), targetName: character.Name, cancellationToken: cancellationToken);

        return Result.Success(CreateCharacterCommandHandler.MapToDto(character, null));
    }
}
