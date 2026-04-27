namespace ShatteredRealms.Application.DTOs.Analytics;

public sealed class ActivityChartDto
{
    public List<ActivityDataPoint> DataPoints { get; set; } = [];
    public List<EventTypeBreakdownPoint> Breakdown { get; set; } = [];
}

public sealed record ActivityDataPoint(DateTime Date, int Count);
public sealed record EventTypeBreakdownPoint(string EventTypeName, int Count);
