using ShatteredRealms.Domain.Entities.Telemetry;

namespace ShatteredRealms.Application.DTOs.Analytics;

public sealed class TelemetryEventDto
{
    public Guid Id { get; set; }
    public TelemetryEventType EventType { get; set; }
    public string EventTypeName => EventType.ToString();
    public string ActorId { get; set; } = string.Empty;
    public string ActorEmail { get; set; } = string.Empty;
    public string? TargetId { get; set; }
    public string? TargetName { get; set; }
    public string? Details { get; set; }
    public DateTime OccurredAt { get; set; }
}
