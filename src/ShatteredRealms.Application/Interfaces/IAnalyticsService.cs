using ShatteredRealms.Domain.Entities.Telemetry;

namespace ShatteredRealms.Application.Interfaces;

public interface IAnalyticsService
{
    Task TrackAsync(
        TelemetryActionType actionType,
        string actorId,
        string actorEmail,
        string? targetId = null,
        string? targetName = null,
        string? details = null,
        CancellationToken cancellationToken = default);
}
