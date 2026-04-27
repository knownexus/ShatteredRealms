namespace ShatteredRealms.Application.DTOs.Analytics;

public sealed class EventAttendanceOverviewDto
{
    public int EventId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
    public bool IsDeleted { get; set; }
    public List<AttendeeDetailDto> CurrentAttendees { get; set; } = [];
    public List<CancellationDetailDto> Cancellations { get; set; } = [];
}

public sealed class AttendeeDetailDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }
}

public sealed class CancellationDetailDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public DateTime CancelledAt { get; set; }
}
