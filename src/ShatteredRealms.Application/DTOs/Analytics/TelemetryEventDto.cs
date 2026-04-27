using ShatteredRealms.Domain.Entities.Telemetry;

namespace ShatteredRealms.Application.DTOs.Analytics;

public sealed class TelemetryEventDto
{
    public Guid Id { get; set; }
    public TelemetryEventType EventType { get; set; }
    public string EventTypeName => EventType.ToString();
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

    // Set by the query handler when a flag rule matches but the event isn't directly flagged
    public bool IsRuleFlagged { get; set; }
    public string? MatchedRuleReason { get; set; }

    public bool AnyFlagged => IsFlagged || IsRuleFlagged;
}
