using System.Collections.Generic;

namespace ShatteredRealms.Application.DTOs.Users;

public class CreateUserRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<string> RoleIds { get; set; } = new();
}
