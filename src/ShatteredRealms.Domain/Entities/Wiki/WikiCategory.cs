namespace ShatteredRealms.Domain.Entities.Wiki;

public class WikiCategory
{
    public int    Id          { get; set; }
    public string Name        { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Navigation
    public ICollection<WikiPageCategory> Pages { get; set; } = new List<WikiPageCategory>();
}