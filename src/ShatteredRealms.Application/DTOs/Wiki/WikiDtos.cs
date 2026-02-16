namespace ShatteredRealms.Application.DTOs.Wiki;

public record WikiPagesDto(List<WikiPageDto> Pages);
public record WikiPageDto(
    int      Id,
    string   Title,
    string   Slug,
    string   CurrentContent,
    string   AuthorId,
    string   AuthorName,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<WikiCategoryDto> Categories,
    int      RevisionCount);

public record WikiRevisionsDto(List<WikiRevisionDto> Revisions);
public record WikiRevisionDto(
    int      Id,
    int      WikiPageId,
    string   EditorId,
    string   EditorName,
    string   RevisionNote,
    DateTime CreatedAt);

public record WikiCategoriesDto(List<WikiCategoryDto> Categories);
public record WikiCategoryDto(int Id, string Name, string Description);

// Requests
public record CreateWikiPageRequest(
    string Title,
    string Content,
    string RevisionNote,
    List<int> CategoryIds);

public record UpdateWikiPageRequest(
    string Title,
    string Content,
    string RevisionNote,
    List<int> CategoryIds);

public record CreateWikiCategoryRequest(string Name, string Description);
