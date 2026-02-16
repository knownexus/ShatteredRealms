namespace ShatteredRealms.Application.DTOs.Forum;


  public record ForumCategoryDto(
      int Id
    , string Name
    , string Description
    , int SortOrder
    , int ThreadCount
    , int PostCount
    , string CreatedById
    , string CreatedByName
    , DateTime CreatedAt);

  public record ForumThreadDto(
      int Id
    , int CategoryId
    , string CategoryName
    , string Title
    , string AuthorId
    , string AuthorName
    , bool IsPinned
    , bool IsLocked
    , int PostCount
    , DateTime CreatedAt
    , DateTime UpdatedAt);

  public record ForumPostDto(
      int Id
    , int ThreadId
    , string AuthorId
    , string AuthorName
    , string Content
    , DateTime CreatedAt
    , DateTime UpdatedAt);

  // Requests
  public record CreateForumCategoryRequest(string Name, string Description, int SortOrder = 0);

  public record UpdateForumCategoryRequest(string Name, string Description, int SortOrder);

  public record CreateForumThreadRequest(int CategoryId, string Title, string InitialPostContent);

  public record UpdateForumThreadRequest(string Title, bool IsPinned, bool IsLocked);

  public record CreateForumPostRequest(int ThreadId, string Content);

  public record UpdateForumPostRequest(string Content);
