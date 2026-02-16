using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Interfaces;

public interface IForumService
{
    // Categories
    Task<Result<List<ForumCategoryDto>>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    Task<Result<ForumCategoryDto>>       GetCategoryAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ForumCategoryDto>>       CreateCategoryAsync(string userId, CreateForumCategoryRequest request, CancellationToken cancellationToken = default);
    Task<Result<ForumCategoryDto>>       UpdateCategoryAsync(int id, UpdateForumCategoryRequest request, CancellationToken cancellationToken = default);
    Task<Result>                         DeleteCategoryAsync(int id, CancellationToken cancellationToken = default);

    // Threads
    Task<Result<List<ForumThreadDto>>> GetThreadsAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<Result<ForumThreadDto>>       GetThreadAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ForumThreadDto>>       CreateThreadAsync(string userId, CreateForumThreadRequest request, CancellationToken cancellationToken = default);
    Task<Result<ForumThreadDto>>       UpdateThreadAsync(int id, string userId, UpdateForumThreadRequest request, CancellationToken cancellationToken = default);
    Task<Result>                       DeleteThreadAsync(int id, CancellationToken cancellationToken = default);

    // Posts
    Task<Result<List<ForumPostDto>>> GetPostsFromThreadAsync(int threadId, CancellationToken cancellationToken = default);
    Task<Result<ForumPostDto>>       GetPostAsync(int postId, CancellationToken cancellationToken = default);
    Task<Result<ForumPostDto>>       CreatePostAsync(string userId, CreateForumPostRequest request, CancellationToken cancellationToken = default);
    Task<Result<ForumPostDto>>       UpdatePostAsync(int id, string userId, string content, CancellationToken cancellationToken = default);
    Task<Result>                     DeletePostAsync(int id, string RequestingUserId, CancellationToken cancellationToken = default);
    Task<Result>                     DeletePostAsAdminAsync(int id, CancellationToken cancellationToken = default);
}
