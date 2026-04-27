namespace ShatteredRealms.Application.DTOs.Analytics;

public sealed class AnalyticsSummaryDto
{
    public int TotalEventsToday { get; set; }
    public int TotalEventsThisWeek { get; set; }
    public int TotalEventsThisMonth { get; set; }
    public int UniqueActorsThisWeek { get; set; }
    public int LoginsToday { get; set; }
    public int LoginsThisWeek { get; set; }
    public List<EventTypeCount> TopEventTypes { get; set; } = [];
    public List<TelemetryEventDto> RecentEvents { get; set; } = [];
}

public sealed class EventTypeCount
{
    public string EventType { get; set; } = string.Empty;
    public int Count { get; set; }
}
