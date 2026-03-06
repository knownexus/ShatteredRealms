namespace ShatteredRealms.Domain.Entities.ActivityLog;

public class ActivityLog
{
    public required Guid Id { get; set; }
    public required string Description { get; set; }
    public required DateTime Date { get; set; }
    public required string UserId { get; set; } = string.Empty;
    public User.User User { get; set; }

}