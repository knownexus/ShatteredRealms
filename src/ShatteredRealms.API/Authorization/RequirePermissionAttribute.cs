using Microsoft.AspNetCore.Authorization;

namespace ShatteredRealms.API.Authorization;

/// <summary>
/// Shorthand for [Authorize(Policy = "permission:X.Y")].
/// Usage: [RequirePermission(Claims.Permissions.Forum.CreateThread)]
/// </summary>
public sealed class RequirePermissionAttribute : AuthorizeAttribute
{
    public RequirePermissionAttribute(string permission)
    {
        Policy = $"permission:{permission}";
    }
}