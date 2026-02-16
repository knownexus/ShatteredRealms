namespace ShatteredRealms.Domain.Entities.Wiki;

public class WikiPageCategory
{
    public int WikiPageId { get; set; }
    public int WikiCategoryId { get; set; }

    // Navigation
    public WikiPage Page { get; set; } = null!;
    public WikiCategory Category { get; set; } = null!;
}