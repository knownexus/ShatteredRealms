namespace ShatteredRealms.Domain.Entities.Telemetry;

public class TelemetryEvent
{
    public Guid Id { get; set; }
    public TelemetryEventType EventType { get; set; }
    public string ActorId { get; set; } = string.Empty;
    public string ActorName { get; set; } = string.Empty;
    public string ActorEmail { get; set; } = string.Empty;
    public string ActorRole { get; set; } = string.Empty;
    public string? TargetId { get; set; }
    public string? TargetName { get; set; }
    public string? Details { get; set; }
    public DateTime OccurredAt { get; set; }

    public bool IsFlagged { get; set; }
    public string? FlagReason { get; set; }
    public DateTime? FlaggedAt { get; set; }
    public string? FlaggedById { get; set; }
}
