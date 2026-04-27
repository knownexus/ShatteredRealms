using MediatR;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.ActivityLog;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class CreateCharacterForUserCommandHandler : IRequestHandler<CreateCharacterForUserCommand, Result<CharacterDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public CreateCharacterForUserCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result<CharacterDto>> Handle(CreateCharacterForUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Result.Failure<CharacterDto>(DomainErrors.Character.NameRequired);

        if (string.IsNullOrWhiteSpace(request.Nationality))
            return Result.Failure<CharacterDto>(DomainErrors.Character.NationalityRequired);

        if (string.IsNullOrWhiteSpace(request.Faction))
            return Result.Failure<CharacterDto>(DomainErrors.Character.FactionRequired);

        var character = new Domain.Entities.Character.Character
        {
            UserId      = request.TargetUserId,
            Name        = request.Name.Trim(),
            Nationality = request.Nationality.Trim(),
            Faction     = request.Faction.Trim(),
            Level       = 1,
            Experience  = 0,
            CreatedAt   = DateTime.UtcNow,
        };

        _context.Character.Add(character);
        await _context.SaveChangesAsync(cancellationToken);

        _context.ActivityLog.Add(new ActivityLog
        {
            Id          = Guid.NewGuid(),
            UserId      = request.RequestingUserId,
            CharacterId = character.Id,
            Description = $"Admin created character '{character.Name}' for user '{request.TargetUserId}'",
            Date        = DateTime.UtcNow,
        });

        await _context.SaveChangesAsync(cancellationToken);

        await _analytics.TrackAsync(TelemetryEventType.CharacterCreated, request.RequestingUserId, string.Empty,
            targetId: character.Id.ToString(), targetName: character.Name,
            details: $"Created for user {request.TargetUserId}", cancellationToken: cancellationToken);

        return Result.Success(CreateCharacterCommandHandler.MapToDto(character, null));
    }
}
