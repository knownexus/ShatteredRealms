using ShatteredRealms.Domain.ValueObjects;

namespace ShatteredRealms.Application.DTOs.Auth;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
