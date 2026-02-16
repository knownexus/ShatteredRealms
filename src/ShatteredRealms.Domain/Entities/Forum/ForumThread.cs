namespace ShatteredRealms.Domain.Entities.Forum;

public class ForumThread
{
    public int      Id         { get; set; }
    public int      CategoryId { get; set; }
    public string   Title      { get; set; } = string.Empty;
    public string   AuthorId   { get; set; } = string.Empty;
    public bool     IsPinned   { get; set; } = false;
    public bool     IsLocked   { get; set; } = false;
    public bool     IsDeleted  { get; set; } = false;
    public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt  { get; set; } = DateTime.UtcNow;

    // Navigation
    public ForumCategory         Category { get; set; } = null!;
    public User.User              Author   { get; set; } = null!;
    public ICollection<ForumPost> Posts   { get; set; } = new List<ForumPost>();
}
