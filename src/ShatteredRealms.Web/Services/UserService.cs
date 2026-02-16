using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShatteredRealms.Application.DTOs.Auth;
using ShatteredRealms.Application.DTOs.Users;
using ShatteredRealms.Domain.Errors;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Web.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var response = await _httpClient.GetAsync("api/users");

        if (!response.IsSuccessStatusCode)
        {
            var statusCode = (int)response.StatusCode;
            var reasonPhrase = response.ReasonPhrase ?? "Unknown error";
            throw new HttpRequestException($"Response status code does not indicate success: {statusCode} ({reasonPhrase}).");
        }

        var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
        return users ?? new List<UserDto>();
    }

    public async Task<UserDto?> GetUserByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<UserDto>($"api/users/{id}");
    }

    public async Task<UserDto?> CreateUserAsync(CreateUserRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users", request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
        return null;
    }

    public async Task<Result<UserDto>> UpdateUserAsync(string id, UpdateUserRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/users/{id}", request);

        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<UserDto>();
            return dto is not null
                ? Result.Success(dto)
                : Result.Failure<UserDto>(new Error(
                                                    $"Call to `api/users/{id}` failed",
                                                    "UpdateUserAsync returned null when attempting to read response",
                                                    (int)HttpStatusCode.InternalServerError));
        }

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<UserDto>(new Error(
                                                 problem?.Title ?? "Unknown error occurred",
                                                 problem?.Detail ?? "No detail provided",
                                                 (int)response.StatusCode));
    }
    public async Task<Result<bool>> DeleteUserAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"api/users/{id}");
        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result.Failure<bool>(new Error(
                                                 problem?.Title ?? "Unknown error occurred",
                                                 problem?.Detail ?? "No detail provided",
                                                 (int)response.StatusCode));
    }
}
