namespace ShatteredRealms.Application.DTOs.Users;

public sealed class UpdateOwnProfileRequest
{
    public string Email     { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName  { get; set; } = string.Empty;
}
