using System.Collections.Generic;

namespace ShatteredRealms.Application.DTOs.Roles;

public class UpdateRoleRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<int> PermissionIds { get; set; } = new();
}
