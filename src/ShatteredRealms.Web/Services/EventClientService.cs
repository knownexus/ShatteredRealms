using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShatteredRealms.Application.DTOs.Events;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Web.Services;

public class EventClientService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;

    public EventClientService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient  = httpClient;
        _apiBaseUrl  = (configuration["ApiSettings:BaseAddress"] ?? "https://localhost:7000").TrimEnd('/');
    }

    public string GetBannerUrl(string? relativePath)
        => string.IsNullOrEmpty(relativePath) ? string.Empty : $"{_apiBaseUrl}/uploads/{relativePath}";

    public async Task<Result<List<EventDto>>> GetAllAsync(bool upcomingOnly = true)
    {
        var response = await _httpClient.GetAsync($"api/events?upcomingOnly={upcomingOnly}");
        if (response.IsSuccessStatusCode)
            return Result.Success(await response.Content.ReadFromJsonAsync<List<EventDto>>() ?? []);

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<List<EventDto>>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load events", (int)response.StatusCode));
    }

    public async Task<Result<EventDto>> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/events/{id}");
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<EventDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<EventDto>(new Error("Event.NotFound", "Event not found", 404));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<EventDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load event", (int)response.StatusCode));
    }

    public async Task<Result<List<EventDto>>> GetMineAsync()
    {
        var response = await _httpClient.GetAsync("api/events/mine");
        if (response.IsSuccessStatusCode)
            return Result.Success(await response.Content.ReadFromJsonAsync<List<EventDto>>() ?? []);

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<List<EventDto>>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load your events", (int)response.StatusCode));
    }

    public async Task<Result<EventDto>> CreateAsync(CreateEventRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/events", request);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<EventDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<EventDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<EventDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to create event", (int)response.StatusCode));
    }

    public async Task<Result<EventDto>> UpdateAsync(int id, UpdateEventRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/events/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<EventDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<EventDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<EventDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to update event", (int)response.StatusCode));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/events/{id}");
        if (response.IsSuccessStatusCode) return Result.Success();

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to delete event", (int)response.StatusCode));
    }

    public async Task<Result> RegisterAsync(int id)
    {
        var response = await _httpClient.PostAsync($"api/events/{id}/register", null);
        if (response.IsSuccessStatusCode) return Result.Success();

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to register", (int)response.StatusCode));
    }

    public async Task<Result> CancelRegistrationAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/events/{id}/register");
        if (response.IsSuccessStatusCode) return Result.Success();

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to cancel registration", (int)response.StatusCode));
    }

    public async Task<Result> UploadBannerAsync(int id, Stream fileStream, string fileName, string contentType)
    {
        using var content  = new MultipartFormDataContent();
        using var sc       = new StreamContent(fileStream);
        sc.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
        content.Add(sc, "file", fileName);

        var response = await _httpClient.PostAsync($"api/events/{id}/banner", content);
        if (response.IsSuccessStatusCode) return Result.Success();

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to upload banner", (int)response.StatusCode));
    }
}
