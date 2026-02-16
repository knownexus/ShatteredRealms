namespace ShatteredRealms.Domain.Entities.User;

public class UserEmergencyContact
{
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = null!;
    public Guid EmergencyContactId { get; set; }
    public EmergencyContact EmergencyContact { get; set; } = null!;
}
