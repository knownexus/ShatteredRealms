namespace ShatteredRealms.Domain.Entities.Wiki;

public class WikiPage
{
    public int      Id             { get; set; }
    public string   Title          { get; set; } = string.Empty;
    public string   Slug           { get; set; } = string.Empty; // URL-safe identifier
    public string   CurrentContent { get; set; } = string.Empty; // Markdown
    public string   AuthorId       { get; set; } = string.Empty;
    public bool     IsDeleted      { get; set; } = false;
    public DateTime CreatedAt      { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt      { get; set; } = DateTime.UtcNow;

    // Navigation
    public User.User                       Author     { get; set; } = null!;
    public ICollection<WikiRevision>  Revisions  { get; set; } = new List<WikiRevision>();
    public ICollection<WikiPageCategory> Categories { get; set; } = new List<WikiPageCategory>();
}
