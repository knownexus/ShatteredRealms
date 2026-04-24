namespace ShatteredRealms.Domain.Entities.Event;

public class EventAttendee
{
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    public User.User User { get; set; } = null!;
    public DateTime RegisteredAt { get; set; }
}
