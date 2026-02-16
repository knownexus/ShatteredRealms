namespace ShatteredRealms.Domain.Entities.Forum;

public class ForumCategory
{
    public int    Id          { get; set; }
    public string Name        { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int    SortOrder   { get; set; } = 0;
    public string CreatedById { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User.User                  CreatedBy { get; set; } = null!;
    public ICollection<ForumThread> Threads { get; set; } = new List<ForumThread>();
}
