using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShatteredRealms.Application.DTOs.Characters;
using ShatteredRealms.Application.DTOs.Positions;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Web.Services;

public class CharacterClientService
{
    private readonly HttpClient _httpClient;

    public CharacterClientService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<List<CharacterDto>>> GetAllAsync(
        string? search = null,
        string? userId = null,
        string? userName = null,
        string? role = null,
        string? nation = null)
    {
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(search)) queryParams.Add($"search={Uri.EscapeDataString(search)}");
        if (!string.IsNullOrEmpty(userId)) queryParams.Add($"userId={Uri.EscapeDataString(userId)}");
        if (!string.IsNullOrEmpty(userName)) queryParams.Add($"userName={Uri.EscapeDataString(userName)}");
        if (!string.IsNullOrEmpty(role)) queryParams.Add($"role={Uri.EscapeDataString(role)}");
        if (!string.IsNullOrEmpty(nation)) queryParams.Add($"nation={Uri.EscapeDataString(nation)}");

        var url = "api/characters";
        if (queryParams.Count > 0)
        {
            url += "?" + string.Join("&", queryParams);
        }

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            return Result.Success(await response.Content.ReadFromJsonAsync<List<CharacterDto>>() ?? []);
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<List<CharacterDto>>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load characters", (int)response.StatusCode));
    }

    public async Task<Result<List<CharacterHistoryDto>>> GetHistoryAsync(int characterId)
    {
        var response = await _httpClient.GetAsync($"api/characters/{characterId}/history");
        if (response.IsSuccessStatusCode)
        {
            return Result.Success(await response.Content.ReadFromJsonAsync<List<CharacterHistoryDto>>() ?? []);
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<List<CharacterHistoryDto>>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load history", (int)response.StatusCode));
    }

    public async Task<Result<List<CharacterDto>>> GetMineAsync()
    {
        var response = await _httpClient.GetAsync("api/characters/self");
        if (response.IsSuccessStatusCode)
        {
            return Result.Success(await response.Content.ReadFromJsonAsync<List<CharacterDto>>() ?? []);
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<List<CharacterDto>>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load characters", (int)response.StatusCode));
    }

    public async Task<Result<List<CharacterDto>>> GetByUserAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"api/characters/user/{userId}");
        if (response.IsSuccessStatusCode)
        {
            return Result.Success(await response.Content.ReadFromJsonAsync<List<CharacterDto>>() ?? []);
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<List<CharacterDto>>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load characters", (int)response.StatusCode));
    }

    public async Task<Result<CharacterDto>> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/characters/{id}");
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<CharacterDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<CharacterDto>(new Error("Character.NotFound", "Character not found", 404));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<CharacterDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load character", (int)response.StatusCode));
    }

    public async Task<Result<CharacterDto>> CreateAsync(CreateCharacterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/characters", request);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<CharacterDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<CharacterDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<CharacterDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to create character", (int)response.StatusCode));
    }

    public async Task<Result<CharacterDto>> UpdateOwnAsync(int id, UpdateCharacterRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/characters/self/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<CharacterDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<CharacterDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<CharacterDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to update character", (int)response.StatusCode));
    }

    public async Task<Result<CharacterDto>> UpdateByAdminAsync(int id, UpdateCharacterByAdminRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/characters/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<CharacterDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<CharacterDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<CharacterDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to update character", (int)response.StatusCode));
    }

    public async Task<Result<CharacterDto>> AssignXpAsync(int characterId, AssignXpRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/characters/{characterId}/assign-xp", request);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<CharacterDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<CharacterDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<CharacterDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to assign XP", (int)response.StatusCode));
    }

    public async Task<Result<CharacterDto>> AssignPositionAsync(int characterId, int? positionId)
    {
        var url = positionId.HasValue
            ? $"api/characters/{characterId}/assign-position/{positionId.Value}"
            : $"api/characters/{characterId}/assign-position";
        var response = await _httpClient.PutAsync(url, null);
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<CharacterDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<CharacterDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<CharacterDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to assign position", (int)response.StatusCode));
    }

    public async Task<Result<CharacterDto>> AssignNationalityAsync(int characterId, string nationality)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/characters/{characterId}/assign-nationality", new { Nationality = nationality });
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<CharacterDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<CharacterDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<CharacterDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to assign nationality", (int)response.StatusCode));
    }

    public async Task<Result<CharacterDto>> AssignFactionAsync(int characterId, string faction)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/characters/{characterId}/assign-faction", new { Faction = faction });
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<CharacterDto>();
            return dto is not null ? Result.Success(dto) : Result.Failure<CharacterDto>(new Error("Error", "Empty response", 500));
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<CharacterDto>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to assign faction", (int)response.StatusCode));
    }

    public async Task<Result<List<PositionDto>>> GetPositionsAsync()
    {
        var response = await _httpClient.GetAsync("api/positions");
        if (response.IsSuccessStatusCode)
        {
            return Result.Success(await response.Content.ReadFromJsonAsync<List<PositionDto>>() ?? []);
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<List<PositionDto>>(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to load positions", (int)response.StatusCode));
    }

    public async Task<Result> DeleteOwnAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/characters/self/{id}");
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to delete character", (int)response.StatusCode));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/characters/{id}");
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        var p = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure(new Error(p?.Title ?? "Error", p?.Detail ?? "Failed to delete character", (int)response.StatusCode));
    }
}
