using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ShatteredRealms.Application.DTOs.Permissions;

namespace ShatteredRealms.Web.Services;

public class PermissionService
{
    private readonly HttpClient _httpClient;

    public PermissionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PermissionDto>> GetAllPermissionsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<PermissionDto>>("api/permissions");
        return response ?? new List<PermissionDto>();
    }
}
