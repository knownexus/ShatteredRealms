namespace ShatteredRealms.Domain.Entities.Event;

public class Event
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
    public User.User CreatedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<EventAttendee> Attendees { get; set; } = new List<EventAttendee>();
}
