using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Services;

public sealed class AnalyticsService : IAnalyticsService
{
    private readonly ApplicationDbContext _context;

    public AnalyticsService(ApplicationDbContext context) => _context = context;

    public async Task TrackAsync(
        TelemetryEventType eventType,
        string actorId,
        string actorEmail,
        string? targetId = null,
        string? targetName = null,
        string? details = null,
        CancellationToken cancellationToken = default)
    {
        _context.TelemetryEvent.Add(new TelemetryEvent
        {
            Id          = Guid.NewGuid(),
            EventType   = eventType,
            ActorId     = actorId,
            ActorEmail  = actorEmail,
            TargetId    = targetId,
            TargetName  = targetName,
            Details     = details,
            OccurredAt  = DateTime.UtcNow,
        });

        await _context.SaveChangesAsync(cancellationToken);
    }
}
