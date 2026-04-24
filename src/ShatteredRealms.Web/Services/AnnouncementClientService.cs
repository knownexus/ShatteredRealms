using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.Application.DTOs.Announcements;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Web.Services;

public class AnnouncementClientService
{
    private readonly HttpClient _httpClient;

    public AnnouncementClientService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<Result<List<AnnouncementDto>>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("api/announcements");
        if (response.IsSuccessStatusCode)
            return Result.Success(await response.Content.ReadFromJsonAsync<List<AnnouncementDto>>() ?? []);

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<List<AnnouncementDto>>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load announcements", (int)response.StatusCode));
    }

    public async Task<Result<AnnouncementDto>> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/announcements/{id}");
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<AnnouncementDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<AnnouncementDto>(new Error("Announcement.NotFound", "Announcement not found", 404));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<AnnouncementDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load announcement", (int)response.StatusCode));
    }

    public async Task<Result<AnnouncementDto>> CreateAsync(CreateAnnouncementRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/announcements", request);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<AnnouncementDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<AnnouncementDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<AnnouncementDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to create announcement", (int)response.StatusCode));
    }

    public async Task<Result<AnnouncementDto>> UpdateAsync(int id, UpdateAnnouncementRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/announcements/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<AnnouncementDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<AnnouncementDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<AnnouncementDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to update announcement", (int)response.StatusCode));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/announcements/{id}");
        if (response.IsSuccessStatusCode) return Result.Success();

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to delete announcement", (int)response.StatusCode));
    }
}
