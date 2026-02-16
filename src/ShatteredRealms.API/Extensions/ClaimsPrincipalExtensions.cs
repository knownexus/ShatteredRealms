using System.Security.Claims;

namespace ShatteredRealms.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal principal)
        => principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "Unknown";

    public static string? GetUserEmail(this ClaimsPrincipal principal)
        => principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "Unknown";

    public static string? GetUserRole(this ClaimsPrincipal principal)
        => principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "Unknown";
}
