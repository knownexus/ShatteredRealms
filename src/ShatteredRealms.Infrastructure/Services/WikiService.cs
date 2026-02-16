using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Application.Interfaces;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.Wiki;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Infrastructure.Data;

namespace ShatteredRealms.Infrastructure.Services;

public class WikiService : IWikiService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<WikiService> _logger;

    public WikiService(ApplicationDbContext context, ILogger<WikiService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<WikiPagesDto>> GetPagesAsync(int? categoryId = null, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading wiki pages{CategoryFilter}", categoryId.HasValue ? $" for category {categoryId}" : "");

        var query = _context.WikiPage
            .Include(p => p.Author)
            .Include(p => p.Categories).ThenInclude(wpc => wpc.Category)
            .Include(p => p.Revisions)
            .AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.Categories.Any(c => c.WikiCategoryId == categoryId.Value));
        }

        var pages = await query
            .OrderByDescending(p => p.UpdatedAt)
            .Select(p => ToDto(p))
            .ToListAsync();

        _logger.LogDebug("Loaded {Count} pages", pages.Count);
        var pagesDto = new WikiPagesDto(pages);

        return pagesDto;
    }

    public async Task<Result<WikiPageDto>> GetPageAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading wiki page {PageId}", id);

        var page = await LoadPage(id);
        if (page is null)
        {
            _logger.LogWarning("Wiki page {PageId} not found", id);
            return Result.Failure<WikiPageDto>(DomainErrors.Wiki.PageNotFound);
        }

        _logger.LogDebug("Wiki page {PageId} loaded successfully", id);
        return Result.Success(ToDto(page));
    }

    public async Task<Result<WikiPageDto>> GetPageBySlugAsync(string slug
                                                            , CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading wiki page by slug '{Slug}'", slug);

        var page = await _context.WikiPage
            .Include(p => p.Author)
            .Include(p => p.Categories).ThenInclude(wpc => wpc.Category)
            .Include(p => p.Revisions)
            .FirstOrDefaultAsync(p => p.Slug == slug);

        if (page is null)
        {
            _logger.LogWarning("Wiki page with slug '{Slug}' not found", slug);
            return Result.Failure<WikiPageDto>(DomainErrors.Wiki.PageNotFound);
        }

        _logger.LogDebug("Wiki page with slug '{Slug}' loaded successfully", slug);
        return Result.Success(ToDto(page));
    }

    public async Task<Result<WikiPageDto>> CreatePageAsync(string userId, CreateWikiPageRequest req
                                                         , CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating wiki page '{Title}' by user {UserId}", req.Title, userId);

        var slug = GenerateSlug(req.Title);
        if (await _context.WikiPage.AnyAsync(p => p.Slug == slug))
        {
            _logger.LogWarning("Cannot create page: slug '{Slug}' already exists", slug);
            return Result.Failure<WikiPageDto>(DomainErrors.Wiki.SlugAlreadyExists);
        }

        var now = DateTime.UtcNow;

        var page = new WikiPage
        {
            Title          = req.Title,
            Slug           = slug,
            CurrentContent = req.Content,
            AuthorId       = userId,
            CreatedAt      = now,
            UpdatedAt      = now,
        };

        _context.WikiPage.Add(page);
        await _context.SaveChangesAsync();

        _logger.LogDebug("Wiki page '{Title}' saved, creating first revision", req.Title);

        _context.WikiRevision.Add(new WikiRevision
        {
            WikiPageId   = page.Id,
            Content      = req.Content,
            EditorId     = userId,
            RevisionNote = req.RevisionNote,
            CreatedAt    = now
        });

        foreach (var catId in req.CategoryIds)
        {
            _context.WikiPageCategory.Add(new WikiPageCategory { WikiPageId = page.Id, WikiCategoryId = catId });
        }

        await _context.SaveChangesAsync();

        var loaded = await LoadPage(page.Id);
        _logger.LogInformation("Wiki page '{Title}' created successfully with ID {PageId}", req.Title, page.Id);
        return Result.Success(ToDto(loaded!));
    }

    public async Task<Result<WikiPageDto>> UpdatePageAsync(int id, string userId, UpdateWikiPageRequest req
                                                         , CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Updating wiki page {PageId} by user {UserId}", id, userId);

        var page = await LoadPage(id);
        if (page is null)
        {
            _logger.LogWarning("Wiki page {PageId} not found", id);
            return Result.Failure<WikiPageDto>(DomainErrors.Wiki.PageNotFound);
        }

        var now = DateTime.UtcNow;

        if (page.Title != req.Title)
        {
            var newSlug = GenerateSlug(req.Title);
            if (await _context.WikiPage.AnyAsync(p => p.Slug == newSlug && p.Id != id))
            {
                _logger.LogWarning("Cannot update page {PageId}: new slug '{Slug}' already exists", id, newSlug);
                return Result.Failure<WikiPageDto>(DomainErrors.Wiki.SlugAlreadyExists);
            }

            page.Title = req.Title;
            page.Slug  = newSlug;
        }

        page.CurrentContent = req.Content;
        page.UpdatedAt      = now;

        _context.WikiRevision.Add(new WikiRevision
        {
            WikiPageId   = id,
            Content      = req.Content,
            EditorId     = userId,
            RevisionNote = req.RevisionNote,
            CreatedAt    = now
        });

        var existingCats = _context.WikiPageCategory.Where(wpc => wpc.WikiPageId == id);
        _context.WikiPageCategory.RemoveRange(existingCats);

        foreach (var catId in req.CategoryIds)
        {
            _context.WikiPageCategory.Add(new WikiPageCategory { WikiPageId = id, WikiCategoryId = catId });
        }

        await _context.SaveChangesAsync();

        var loaded = await LoadPage(id);
        _logger.LogInformation("Wiki page {PageId} updated successfully", id);
        return Result.Success(ToDto(loaded!));
    }

    public async Task<Result> DeletePageAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Deleting wiki page {PageId}", id);

        var page = await _context.WikiPage.FindAsync(id);
        if (page is null)
        {
            _logger.LogWarning("Wiki page {PageId} not found", id);
            return Result.Failure(DomainErrors.Wiki.PageNotFound);
        }

        page.IsDeleted = true;
        await _context.SaveChangesAsync();

        _logger.LogInformation("Wiki page {PageId} marked as deleted", id);
        return Result.Success();
    }

    public async Task<Result<WikiRevisionsDto>> GetRevisionsAsync(int pageId
                                                             , CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading revisions for wiki page {PageId}", pageId);

        var revisions = await _context.WikiRevision
            .Where(r => r.WikiPageId == pageId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new WikiRevisionDto(
                r.Id, r.WikiPageId, r.EditorId,
                r.Editor.FirstName + " " + r.Editor.LastName,
                r.RevisionNote, r.CreatedAt))
            .ToListAsync();

        _logger.LogDebug("Loaded {Count} revisions for page {PageId}", revisions.Count, pageId);
        var revisionsDto = new WikiRevisionsDto(revisions);
        return revisionsDto;
    }

    public async Task<Result<WikiCategoriesDto>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading wiki categories");

        var categories = await _context.WikiCategory
                                       .OrderBy(c => c.Name)
                                       .Select(c => new WikiCategoryDto(c.Id, c.Name, c.Description))
                                       .ToListAsync();

        _logger.LogDebug("Loaded {Count} wiki categories", categories.Count);
        var categoriesDto = new WikiCategoriesDto(categories);
        return categoriesDto;
    }


    public async Task<Result<WikiCategoryDto>> CreateCategoryAsync(CreateWikiCategoryRequest req
                                                                 , CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Creating wiki category '{CategoryName}'", req.Name);

        var category = new WikiCategory { Name = req.Name, Description = req.Description };
        _context.WikiCategory.Add(category);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Wiki category '{CategoryName}' created successfully with ID {CategoryId}", req.Name, category.Id);
        return Result.Success(new WikiCategoryDto(category.Id, category.Name, category.Description));
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private Task<WikiPage?> LoadPage(int id) =>
        _context.WikiPage
            .Include(p => p.Author)
            .Include(p => p.Categories).ThenInclude(wpc => wpc.Category)
            .Include(p => p.Revisions)
            .FirstOrDefaultAsync(p => p.Id == id);

    private static WikiPageDto ToDto(WikiPage p) =>
        new(
            p.Id,
            p.Title,
            p.Slug,
            p.CurrentContent,
            p.AuthorId,
            p.Author.FirstName + " " + p.Author.LastName,
            p.CreatedAt,
            p.UpdatedAt,
            p.Categories.Select(wpc => new WikiCategoryDto(wpc.WikiCategoryId, wpc.Category.Name, wpc.Category.Description)).ToList(),
            p.Revisions.Count
        );

    private static string GenerateSlug(string title) =>
        System.Text.RegularExpressions.Regex.Replace(title.ToLowerInvariant().Trim(), @"[^a-z0-9]+", "-").Trim('-');
}