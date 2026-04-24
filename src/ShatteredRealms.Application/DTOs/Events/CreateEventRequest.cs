namespace ShatteredRealms.Application.DTOs.Events;

public class CreateEventRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string? Location { get; set; }
    public int? MemberCap { get; set; }
}
