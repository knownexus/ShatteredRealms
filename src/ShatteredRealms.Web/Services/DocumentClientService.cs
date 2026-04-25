using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.Application.DTOs.Documents;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Web.Services;

public class DocumentClientService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;

    public DocumentClientService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient  = httpClient;
        _apiBaseUrl  = (configuration["ApiSettings:BaseAddress"] ?? "https://localhost:7000").TrimEnd('/');
    }

    public string GetDownloadUrl(int id) => $"{_apiBaseUrl}/api/documents/{id}/download";

    public async Task<Result<List<DocumentDto>>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("api/documents");
        if (response.IsSuccessStatusCode)
            return Result.Success(await response.Content.ReadFromJsonAsync<List<DocumentDto>>() ?? []);

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<List<DocumentDto>>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load documents", (int)response.StatusCode));
    }

    public async Task<Result<DocumentDto>> UploadAsync(Stream fileStream, string fileName, string contentType, long fileSize)
    {
        using var content = new MultipartFormDataContent();
        using var sc = new StreamContent(fileStream);
        sc.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
        sc.Headers.ContentLength = fileSize;
        content.Add(sc, "file", fileName);

        var response = await _httpClient.PostAsync("api/documents", content);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<DocumentDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<DocumentDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<DocumentDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to upload document", (int)response.StatusCode));
    }

    public async Task<Result<(byte[] Bytes, string ContentType)>> GetBytesAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/documents/{id}/download");
        if (response.IsSuccessStatusCode)
        {
            var bytes = await response.Content.ReadAsByteArrayAsync();
            var ct = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
            return Result.Success((bytes, ct));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<(byte[], string)>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load document", (int)response.StatusCode));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/documents/{id}");
        if (response.IsSuccessStatusCode) return Result.Success();

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to delete document", (int)response.StatusCode));
    }
}
