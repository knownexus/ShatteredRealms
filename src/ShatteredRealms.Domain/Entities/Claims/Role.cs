using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ShatteredRealms.Domain.Entities.User;

namespace ShatteredRealms.Domain.Entities;

public class Role : IdentityRole
{
    public string Description { get; set; } = string.Empty;
    public int Priority { get; set; } = 0;
    public bool IsSystem { get; set; } = false;

    // Navigation properties
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}