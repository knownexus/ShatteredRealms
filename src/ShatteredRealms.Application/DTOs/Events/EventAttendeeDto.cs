namespace ShatteredRealms.Application.DTOs.Events;

public class EventAttendeeDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }
}
