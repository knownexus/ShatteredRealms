using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.Application.DTOs.Forum;
using ShatteredRealms.Domain.Shared;
using ShatteredRealms.Web.Helpers;

namespace ShatteredRealms.Web.Services;

public class ForumClientService(HttpClient http)
{
    public Task<List<ForumCategoryDto>> GetCategoriesAsync() =>
        http.GetFromJsonAsync<List<ForumCategoryDto>>("api/forum/categories")!;

    public Task<List<ForumThreadDto>> GetThreadsAsync(int categoryId) =>
        http.GetFromJsonAsync<List<ForumThreadDto>>($"api/forum/categories/{categoryId}/threads")!;

    public Task<List<ForumPostDto>> GetPostsAsync(int threadId) =>
        http.GetFromJsonAsync<List<ForumPostDto>>($"api/forum/threads/{threadId}/posts")!;

    public Task<Result<ForumCategoryDto>> GetCategoryAsync(int id) =>
        HttpHelpers.GetResult<ForumCategoryDto>(() => http.GetAsync($"api/forum/categories/{id}"));

    public Task<Result<ForumCategoryDto>> CreateCategoryAsync(CreateForumCategoryRequest request) =>
        HttpHelpers.GetResult<ForumCategoryDto>(() => http.PostAsJsonAsync("api/forum/categories", request));

    public Task<Result<ForumCategoryDto>> UpdateCategoryAsync(int id, UpdateForumCategoryRequest request) =>
        HttpHelpers.GetResult<ForumCategoryDto>(() => http.PutAsJsonAsync($"api/forum/categories/{id}", request));

    public async Task<Result> DeleteCategoryAsync(int id)
    {
        var response = await http.DeleteAsync($"api/forum/categories/{id}");

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

    public Task<Result<ForumThreadDto>> GetThreadAsync(int id) =>
        HttpHelpers.GetResult<ForumThreadDto>(() => http.GetAsync($"api/forum/threads/{id}"));

    public Task<Result<ForumThreadDto>> CreateThreadAsync(CreateForumThreadRequest request) =>
        HttpHelpers.GetResult<ForumThreadDto>(() => http.PostAsJsonAsync("api/forum/threads", request));

    public async Task DeleteThreadAsync(int id)
        => (await http.DeleteAsync($"api/forum/threads/{id}")).EnsureSuccessStatusCode();

    public Task<Result<ForumPostDto>> CreatePostAsync(CreateForumPostRequest request) =>
        HttpHelpers.GetResult<ForumPostDto>(() => http.PostAsJsonAsync("api/forum/posts", request));

    public Task<Result<ForumPostDto>> UpdatePostAsync(int id, string content) =>
        HttpHelpers.GetResult<ForumPostDto>(() => http.PutAsJsonAsync($"api/forum/posts/{id}"
                                                                    , new UpdateForumPostRequest(content)));

    public async Task<Result> DeletePostAsync(int id)
    {
        var response = await http.DeleteAsync($"api/forum/posts/{id}");

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
}