using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.Forum;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Services;

public class ForumService : IForumService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ForumService> _logger;

    public ForumService(ApplicationDbContext context, ILogger<ForumService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // ── Categories ────────────────────────────────────────────────────────────

    public async Task<Result<List<ForumCategoryDto>>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading all forum categories");

        var categories = await _context.ForumCategory
            .Include(c => c.Threads)
                .ThenInclude(t => t.Posts)
            .Include(c => c.CreatedBy)
            .OrderBy(c => c.SortOrder)
            .ThenBy(c => c.Name)
            .ToListAsync();

        var categoriesDto = categories
            .Select(c => ToDto(c))
            .ToList();

        _logger.LogDebug("Loaded {Count} forum categories", categoriesDto.Count);
        return Result.Success(categoriesDto);
    }

    public async Task<Result<ForumCategoryDto>> GetCategoryAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading category {CategoryId}", id);

        var category = await _context.ForumCategory
            .Include(c => c.CreatedBy)
            .Include(c => c.Threads)
                .ThenInclude(t => t.Posts)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
        {
            _logger.LogWarning("Category {CategoryId} not found", id);
            return Result.Failure<ForumCategoryDto>(DomainErrors.Forum.CategoryNotFound);
        }

        var dto = ToDto(category);
        _logger.LogDebug("Category {CategoryId} loaded successfully", id);
        return Result.Success(dto);
    }

    public async Task<Result<ForumCategoryDto>> CreateCategoryAsync(string userId, CreateForumCategoryRequest req, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Creating new category '{CategoryName}' by user {UserId}", req.Name, userId);

        var category = new ForumCategory
        {
            Name        = req.Name,
            Description = req.Description,
            SortOrder   = req.SortOrder,
            CreatedById = userId,
            CreatedAt   = DateTime.UtcNow
        };

        _context.ForumCategory.Add(category);
        await _context.SaveChangesAsync();

        await _context.Entry(category).Reference(c => c.CreatedBy).LoadAsync();

        var dto = ToDto(category);
        _logger.LogDebug("Category '{CategoryName}' created successfully with ID {CategoryId}", req.Name, dto.Id);
        return Result.Success(dto);
    }

    public async Task<Result<ForumCategoryDto>> UpdateCategoryAsync(int id, UpdateForumCategoryRequest req, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Updating category {CategoryId}", id);

        var category = await _context.ForumCategory
            .Include(c => c.CreatedBy)
            .Include(c => c.Threads)
                .ThenInclude(t => t.Posts)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
        {
            _logger.LogWarning("Category {CategoryId} not found for update", id);
            return Result.Failure<ForumCategoryDto>(DomainErrors.Forum.CategoryNotFound);
        }

        category.Name        = req.Name;
        category.Description = req.Description;
        category.SortOrder   = req.SortOrder;

        await _context.SaveChangesAsync();

        var dto = ToDto(category);
        _logger.LogDebug("Category {CategoryId} updated successfully", id);
        return Result.Success(dto);
    }

    public async Task<Result> DeleteCategoryAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Deleting category {CategoryId}", id);

        var category = await _context.ForumCategory.FindAsync(id);
        if (category is null)
        {
            _logger.LogWarning("Category {CategoryId} not found for deletion", id);
            return Result.Failure(DomainErrors.Forum.CategoryNotFound);
        }

        _context.ForumCategory.Remove(category);
        await _context.SaveChangesAsync();

        _logger.LogDebug("Category {CategoryId} deleted successfully", id);
        return Result.Success();
    }

    // ── Threads ───────────────────────────────────────────────────────────────

    public async Task<Result<List<ForumThreadDto>>> GetThreadsAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading threads for category {CategoryId}", categoryId);

        var threads = await _context.ForumThread
            .Include(t => t.Author)
            .Include(t => t.Posts)
            .Include(t => t.Category)
            .Where(t => t.CategoryId == categoryId)
            .OrderByDescending(t => t.IsPinned)
            .ThenByDescending(t => t.UpdatedAt)
            .ToListAsync();

        var dtos = threads.Select(t => ToThreadDto(t)).ToList();
        _logger.LogDebug("Loaded {Count} threads for category {CategoryId}", dtos.Count, categoryId);
        return Result.Success(dtos);
    }

    public async Task<Result<ForumThreadDto>> GetThreadAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading thread {ThreadId}", id);

        var thread = await _context.ForumThread
            .Include(t => t.Author)
            .Include(t => t.Category)
            .Include(t => t.Posts)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (thread is null)
        {
            _logger.LogWarning("Thread {ThreadId} not found", id);
            return Result.Failure<ForumThreadDto>(DomainErrors.Forum.ThreadNotFound);
        }

        var dto = ToThreadDto(thread);
        _logger.LogDebug("Thread {ThreadId} loaded successfully", id);
        return Result.Success(dto);
    }

    public async Task<Result<ForumThreadDto>> CreateThreadAsync(string userId, CreateForumThreadRequest req, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Creating new thread in category {CategoryId} by user {UserId}", req.CategoryId, userId);

        var category = await _context.ForumCategory.FindAsync(req.CategoryId);
        if (category is null)
        {
            _logger.LogWarning("Category {CategoryId} not found for thread creation", req.CategoryId);
            return Result.Failure<ForumThreadDto>(DomainErrors.Forum.CategoryNotFound);
        }

        var now = DateTime.UtcNow;
        var thread = new ForumThread
        {
            CategoryId = req.CategoryId,
            Title      = req.Title,
            AuthorId   = userId,
            CreatedAt  = now,
            UpdatedAt  = now
        };

        _context.ForumThread.Add(thread);
        await _context.SaveChangesAsync();

        var post = new ForumPost
        {
            ThreadId  = thread.Id,
            AuthorId  = userId,
            Content   = req.InitialPostContent,
            CreatedAt = now,
            UpdatedAt = now
        };

        _context.ForumPost.Add(post);
        await _context.SaveChangesAsync();

        await _context.Entry(thread).Reference(t => t.Author).LoadAsync();
        await _context.Entry(thread).Reference(t => t.Category).LoadAsync();

        var dto = ToThreadDto(thread);
        _logger.LogDebug("Thread '{ThreadTitle}' created successfully with ID {ThreadId}", thread.Title, dto.Id);
        return Result.Success(dto);
    }

    public async Task<Result<ForumThreadDto>> UpdateThreadAsync(int id, string userId, UpdateForumThreadRequest req, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Updating thread {ThreadId} by user {UserId}", id, userId);

        var thread = await _context.ForumThread
            .Include(t => t.Author)
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (thread is null)
        {
            _logger.LogWarning("Thread {ThreadId} not found for update", id);
            return Result.Failure<ForumThreadDto>(DomainErrors.Forum.ThreadNotFound);
        }

        thread.Title    = req.Title;
        thread.IsPinned = req.IsPinned;
        thread.IsLocked = req.IsLocked;
        thread.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var dto = ToThreadDto(thread);
        _logger.LogDebug("Thread {ThreadId} updated successfully", id);
        return Result.Success(dto);
    }

    public async Task<Result> DeleteThreadAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Deleting thread {ThreadId}", id);

        var thread = await _context.ForumThread.FindAsync(id);
        if (thread is null)
        {
            _logger.LogWarning("Thread {ThreadId} not found for deletion", id);
            return Result.Failure(DomainErrors.Forum.ThreadNotFound);
        }

        thread.IsDeleted = true;
        await _context.SaveChangesAsync();

        _logger.LogDebug("Thread {ThreadId} marked as deleted", id);
        return Result.Success();
    }

    // ── Posts ─────────────────────────────────────────────────────────────────

    public async Task<Result<List<ForumPostDto>>> GetPostsFromThreadAsync(int threadId, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading posts for thread {ThreadId}", threadId);

        var posts = await _context.ForumPost
            .Include(p => p.Author)
            .Where(p => p.ThreadId == threadId)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();

        var dtos = posts.Select(p => new ForumPostDto(
            p.Id, p.ThreadId, p.AuthorId,
            p.Author.FirstName + " " + p.Author.LastName,
            p.Content, p.CreatedAt, p.UpdatedAt)).ToList();

        _logger.LogDebug("Loaded {Count} posts for thread {ThreadId}", dtos.Count, threadId);
        return Result.Success(dtos);
    }

    public async Task<Result<ForumPostDto>> GetPostAsync(int postId, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading post {PostId}", postId);

        var post = await _context.ForumPost
            .Include(p => p.Author)
            .FirstOrDefaultAsync(p => p.Id == postId);

        if (post is null)
        {
            _logger.LogWarning("Post {PostId} not found", postId);
            return Result.Failure<ForumPostDto>(DomainErrors.Forum.PostNotFound);
        }

        var dto = new ForumPostDto(
            post.Id, post.ThreadId, post.AuthorId,
            post.Author.FirstName + " " + post.Author.LastName,
            post.Content, post.CreatedAt, post.UpdatedAt);

        _logger.LogDebug("Post {PostId} loaded successfully", postId);
        return Result.Success(dto);
    }

    public async Task<Result<ForumPostDto>> CreatePostAsync(string userId, CreateForumPostRequest req, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Creating post in thread {ThreadId} by user {UserId}", req.ThreadId, userId);

        var thread = await _context.ForumThread.FindAsync(req.ThreadId);
        if (thread is null)
        {
            _logger.LogWarning("Thread {ThreadId} not found for post creation", req.ThreadId);
            return Result.Failure<ForumPostDto>(DomainErrors.Forum.ThreadNotFound);
        }

        if (thread.IsLocked)
        {
            _logger.LogWarning("Thread {ThreadId} is locked. Cannot create post", req.ThreadId);
            return Result.Failure<ForumPostDto>(DomainErrors.Forum.ThreadLocked);
        }

        var now = DateTime.UtcNow;
        var post = new ForumPost
        {
            ThreadId  = req.ThreadId,
            AuthorId  = userId,
            Content   = req.Content,
            CreatedAt = now,
            UpdatedAt = now
        };

        _context.ForumPost.Add(post);
        thread.UpdatedAt = now;
        await _context.SaveChangesAsync();

        await _context.Entry(post).Reference(p => p.Author).LoadAsync();

        var dto = new ForumPostDto(
            post.Id, post.ThreadId, post.AuthorId,
            post.Author.FirstName + " " + post.Author.LastName,
            post.Content, post.CreatedAt, post.UpdatedAt);

        _logger.LogDebug("Post {PostId} created successfully in thread {ThreadId}", dto.Id, req.ThreadId);
        return Result.Success(dto);
    }

    public async Task<Result<ForumPostDto>> UpdatePostAsync(int id, string userId, string content, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Updating post {PostId} by user {UserId}", id, userId);

        var post = await _context.ForumPost
            .Include(p => p.Author)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post is null)
        {
            _logger.LogWarning("Post {PostId} not found for update", id);
            return Result.Failure<ForumPostDto>(DomainErrors.Forum.PostNotFound);
        }

        post.Content   = content;
        post.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var dto = new ForumPostDto(
            post.Id, post.ThreadId, post.AuthorId,
            post.Author.FirstName + " " + post.Author.LastName,
            post.Content, post.CreatedAt, post.UpdatedAt);

        _logger.LogDebug("Post {PostId} updated successfully", id);
        return Result.Success(dto);
    }

    public async Task<Result> DeletePostAsync(int id, string requestingUserId, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Deleting post {PostId}", id);

        var post = await _context.ForumPost.FindAsync(id);
        if (post is null)
        {
            _logger.LogWarning("Post {PostId} not found for deletion", id);
            return Result.Failure(DomainErrors.Forum.PostNotFound);
        }

        var isAuthor = post.AuthorId == requestingUserId;

        if (!isAuthor)
        {
            return Result.Failure(DomainErrors.Forum.CannotEditOthers);
        }

        post.IsDeleted = true;
        await _context.SaveChangesAsync();

        _logger.LogDebug("Post {PostId} marked as deleted", id);
        return Result.Success();
    }

    public async Task<Result> DeletePostAsAdminAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Deleting post as Admin {PostId}", id);

        var post = await _context.ForumPost.FindAsync(id);
        if (post is null)
        {
            _logger.LogWarning("Post {PostId} not found for deletion", id);
            return Result.Failure(DomainErrors.Forum.PostNotFound);
        }

        post.IsDeleted = true;
        await _context.SaveChangesAsync();

        _logger.LogDebug("Post {PostId} marked as deleted", id);
        return Result.Success();
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static ForumCategoryDto ToDto(ForumCategory c) =>
        new(
            c.Id,
            c.Name,
            c.Description,
            c.SortOrder,
            c.Threads.Count(t => !t.IsDeleted),
            c.Threads.SelectMany(t => t.Posts).Count(p => !p.IsDeleted),
            c.CreatedById,
            c.CreatedBy.FirstName + " " + c.CreatedBy.LastName,
            c.CreatedAt);

    private static ForumThreadDto ToThreadDto(ForumThread t) =>
        new(
            t.Id,
            t.CategoryId,
            t.Category?.Name ?? string.Empty,
            t.Title,
            t.AuthorId,
            t.Author is not null ? t.Author.FirstName + " " + t.Author.LastName : string.Empty,
            t.IsPinned,
            t.IsLocked,
            t.Posts.Count(p => !p.IsDeleted),
            t.CreatedAt,
            t.UpdatedAt);
}