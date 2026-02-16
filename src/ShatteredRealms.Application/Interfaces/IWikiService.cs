using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Application.Interfaces;

public interface IWikiService
{
    Task<Result<WikiPagesDto>>      GetPagesAsync(int? categoryId = null, CancellationToken cancellationToken = default);
    Task<Result<WikiPageDto>>       GetPageAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<WikiPageDto>>       GetPageBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<Result<WikiPageDto>>       CreatePageAsync(string userId, CreateWikiPageRequest request, CancellationToken cancellationToken = default);
    Task<Result<WikiPageDto>>       UpdatePageAsync(int id, string userId, UpdateWikiPageRequest request, CancellationToken cancellationToken = default);
    Task<Result>                    DeletePageAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<WikiRevisionsDto>>  GetRevisionsAsync(int pageId, CancellationToken cancellationToken = default);
    Task<Result<WikiCategoriesDto>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    Task<Result<WikiCategoryDto>>   CreateCategoryAsync(CreateWikiCategoryRequest request, CancellationToken cancellationToken = default);
}
