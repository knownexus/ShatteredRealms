namespace ShatteredRealms.Domain.Entities.User;

public class EmergencyContact
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string EmailAddress { get; set; }
    public required string PhoneNumber { get; set; }
    public ICollection<UserEmergencyContact> UserEmergencyContact { get; set; } = new List<UserEmergencyContact>();

}