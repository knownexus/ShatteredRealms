using Microsoft.AspNetCore.Identity;

namespace ShatteredRealms.Domain.Entities.User;

public class UserRole : IdentityUserRole<string>
{
    // Navigation properties
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}