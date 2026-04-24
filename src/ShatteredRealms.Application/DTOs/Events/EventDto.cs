namespace ShatteredRealms.Application.DTOs.Events;

public class EventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string? Location { get; set; }
    public string? BannerImagePath { get; set; }
    public int? MemberCap { get; set; }
    public string CreatedById { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int AttendeeCount { get; set; }
    public bool IsGoing { get; set; }
}
