namespace ShatteredRealms.Domain.Entities.Telemetry;

public class AnalyticsFlagRule
{
    public int Id { get; set; }
    public FlagRuleType RuleType { get; set; }
    public string? TargetUserId { get; set; }
    public TelemetryEventType? EventType { get; set; }
    public string? ActorRole { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string CreatedById { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}

public enum FlagRuleType
{
    User      = 1,
    EventType = 2,
    RoleAction = 3,
}
