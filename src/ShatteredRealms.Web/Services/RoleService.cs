using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ShatteredRealms.Application.DTOs.Roles;

namespace ShatteredRealms.Web.Services;

public class RoleService
{
    private readonly HttpClient _httpClient;

    public RoleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RoleDto>> GetAllRolesAsync()
    {
        var response = await _httpClient.GetAsync("api/roles");

        if (!response.IsSuccessStatusCode)
        {
            var statusCode = (int)response.StatusCode;
            var reasonPhrase = response.ReasonPhrase ?? "Unknown error";
            throw new HttpRequestException($"Response status code does not indicate success: {statusCode} ({reasonPhrase}).");
        }

        var roles = await response.Content.ReadFromJsonAsync<List<RoleDto>>();
        return roles ?? new List<RoleDto>();
    }

    public async Task<RoleDto?> GetRoleByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<RoleDto>($"api/roles/{id}");
    }

    public async Task<RoleDto?> CreateRoleAsync(CreateRoleRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/roles", request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<RoleDto>();
        }
        return null;
    }

    public async Task<RoleDto?> UpdateRoleAsync(string id, UpdateRoleRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/roles/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<RoleDto>();
        }
        return null;
    }

    public async Task<bool> DeleteRoleAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"api/roles/{id}");
        return response.IsSuccessStatusCode;
    }
}
