namespace ShatteredRealms.Domain.Entities.Forum;

public class ForumPost
{
    public int      Id        { get; set; }
    public int      ThreadId  { get; set; }
    public string   AuthorId  { get; set; } = string.Empty;
    public string   Content   { get; set; } = string.Empty; // Markdown
    public bool     IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ForumThread Thread { get; set; } = null!;
    public User.User        Author { get; set; } = null!;
}
