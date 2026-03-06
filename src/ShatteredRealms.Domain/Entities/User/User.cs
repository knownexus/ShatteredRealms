using Microsoft.AspNetCore.Identity;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Domain.Entities.User;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public ICollection<UserEmergencyContact> UserEmergencyContact { get; set; } = new List<UserEmergencyContact>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<ActivityLog.ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog.ActivityLog>();

    public static Result Validate(User user)
    {
        var firstName = ValueObjects.FirstName.Create(user.FirstName);
        if (firstName.IsFailure)
        {
            return firstName;
        }

        var lastName = ValueObjects.LastName.Create(user.LastName);
        if (lastName.IsFailure)
        {
            return lastName;
        }

        var email = ValueObjects.Email.Create(user.Email ?? string.Empty);
        if (email.IsFailure)
        {
            return email;
        }

        return Result.Success();
    }
}
