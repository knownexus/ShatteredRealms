namespace ShatteredRealms.Domain.Entities.Wiki;

public class WikiRevision
{
    public int      Id           { get; set; }
    public int      WikiPageId   { get; set; }
    public string   Content      { get; set; } = string.Empty; // Markdown snapshot
    public string   EditorId     { get; set; } = string.Empty;
    public string   RevisionNote { get; set; } = string.Empty;
    public DateTime CreatedAt    { get; set; } = DateTime.UtcNow;

    // Navigation
    public WikiPage Page   { get; set; } = null!;
    public User.User     Editor { get; set; } = null!;
}
