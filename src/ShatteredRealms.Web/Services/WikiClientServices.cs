using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.Application.DTOs.Wiki;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Web.Helpers;

namespace ShatteredRealms.Web.Services;

public class WikiClientService(HttpClient http)
{
    public Task<WikiPagesDto> GetPagesAsync(int? categoryId = null) =>
        http.GetFromJsonAsync<WikiPagesDto>($"api/wiki/pages{(categoryId.HasValue ? $"?categoryId={categoryId}" : "")}")!;

    public Task<WikiRevisionsDto> GetRevisionsAsync(int pageId) =>
        http.GetFromJsonAsync<WikiRevisionsDto>($"api/wiki/pages/{pageId}/revisions")!;

    public Task<WikiCategoriesDto> GetCategoriesAsync() =>
        http.GetFromJsonAsync<WikiCategoriesDto>("api/wiki/categories")!;

    public Task<Result<WikiPageDto>> GetPageBySlugAsync(string slug) =>
        HttpHelpers.GetResult<WikiPageDto>(() => http.GetAsync($"api/wiki/pages/slug/{slug}"));

    public Task<Result<WikiPageDto>> GetPageAsync(int id) =>
        HttpHelpers.GetResult<WikiPageDto>(() => http.GetAsync($"api/wiki/pages/{id}"));

    public Task<Result<WikiPageDto>> CreatePageAsync(CreateWikiPageRequest request) =>
        HttpHelpers.GetResult<WikiPageDto>(() => http.PostAsJsonAsync("api/wiki/pages", request));

    public Task<Result<WikiPageDto>> UpdatePageAsync(int id, UpdateWikiPageRequest request) =>
        HttpHelpers.GetResult<WikiPageDto>(() => http.PutAsJsonAsync($"api/wiki/pages/{id}", request));

    public async Task<Result> DeletePageAsync(int id)
    {
        var response = await http.DeleteAsync($"api/wiki/pages/{id}");

        if (response.IsSuccessStatusCode)
            return Result.Success();

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        if (problem != null)
        {
            return Result.Failure(new Error(
                                            problem.Title ?? "Error",
                                            problem.Detail ?? "Unknown error",
                                            problem.Status ?? (int)response.StatusCode));
        }

        // fallback
        return Result.Failure(new Error(
                                        "Error",
                                        response.ReasonPhrase ?? "Unknown error",
                                        (int)response.StatusCode));
    }

    public Task<Result<WikiCategoryDto>> CreateCategoryAsync(CreateWikiCategoryRequest request) =>
        HttpHelpers.GetResult<WikiCategoryDto>(() => http.PostAsJsonAsync("api/wiki/categories", request));
}