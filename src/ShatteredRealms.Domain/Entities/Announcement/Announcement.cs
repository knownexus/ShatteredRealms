namespace ShatteredRealms.Domain.Entities.Announcement;

public class Announcement
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public int? LinkedEventId { get; set; }
    public Event.Event? LinkedEvent { get; set; }
    public string AuthorId { get; set; } = string.Empty;
    public User.User Author { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
