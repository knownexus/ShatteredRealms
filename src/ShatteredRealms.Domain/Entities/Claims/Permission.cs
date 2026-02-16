using Microsoft.AspNetCore.Identity;

namespace ShatteredRealms.Domain.Entities;

public class Permission : IdentityRoleClaim<string>
{
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;

    // Convenience factory so call-sites stay readable
    public static Permission For(string roleId, string name, string description, string category) =>
        new()
        {
            RoleId      = roleId,
            ClaimType   = "permission",
            ClaimValue  = name,
            Description = description,
            Category    = category
        };
}