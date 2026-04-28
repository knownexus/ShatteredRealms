using MediatR;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Application.Features.Characters.Commands;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.ActivityLog;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Handlers.Characters;

public sealed class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, Result>
{
    private readonly ApplicationDbContext _context;
    private readonly IAnalyticsService _analytics;

    public DeleteCharacterCommandHandler(ApplicationDbContext context, IAnalyticsService analytics)
    {
        _context = context;
        _analytics = analytics;
    }

    public async Task<Result> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await _context.Character
            .FirstOrDefaultAsync(c => c.Id == request.CharacterId, cancellationToken);

        if (character is null)
        {
            return Result.Failure(DomainErrors.Character.NotFound);
        }

        var name = character.Name;
        _context.Character.Remove(character);

        _context.ActivityLog.Add(new ActivityLog
        {
            Id          = Guid.NewGuid(),
            UserId      = request.RequestingUserId,
            CharacterId = null,
            Description = $"Deleted character '{name}' (id: {request.CharacterId})",
            Date        = DateTime.UtcNow,
        });

        await _context.SaveChangesAsync(cancellationToken);

        await _analytics.TrackAsync(TelemetryEventType.CharacterDeleted, request.RequestingUserId, string.Empty,
            targetId: request.CharacterId.ToString(), targetName: name, cancellationToken: cancellationToken);

        return Result.Success();
    }
}
