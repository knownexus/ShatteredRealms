using ShatteredRealms.Domain.Entities.Telemetry;

namespace ShatteredRealms.Application.Interfaces;

public interface IAnalyticsService
{
    Task TrackAsync(
        TelemetryEventType eventType,
        string actorId,
        string actorEmail,
        string? targetId = null,
        string? targetName = null,
        string? details = null,
        CancellationToken cancellationToken = default);
}
