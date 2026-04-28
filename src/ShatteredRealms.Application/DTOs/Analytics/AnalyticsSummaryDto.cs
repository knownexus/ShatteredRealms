namespace ShatteredRealms.Application.DTOs.Analytics;

public sealed class AnalyticsSummaryDto
{
    public int TotalActionsToday { get; set; }
    public int TotalActionsThisWeek { get; set; }
    public int TotalActionsThisMonth { get; set; }
    public int UniqueUsersThisWeek { get; set; }
    public int LoginsToday { get; set; }
    public int LoginsThisWeek { get; set; }
    public List<EventTypeCount> TopActionTypes { get; set; } = [];
    public List<TelemetryActionDto> RecentEvents { get; set; } = [];
}

public sealed class EventTypeCount
{
    public string ActionType { get; set; } = string.Empty;
    public int Count { get; set; }
}
