using ShatteredRealms.Domain.Entities.Telemetry;

namespace ShatteredRealms.Application.DTOs.Analytics;

public sealed class AnalyticsFlagRuleDto
{
    public int Id { get; set; }
    public FlagRuleType RuleType { get; set; }
    public string RuleTypeName => RuleType.ToString();
    public string? TargetUserId { get; set; }
    public string? TargetUserName { get; set; }
    public TelemetryEventType? EventType { get; set; }
    public string? EventTypeName => EventType?.ToString();
    public string? ActorRole { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string CreatedById { get; set; } = string.Empty;
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}
