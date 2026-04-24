namespace ShatteredRealms.Application.DTOs.Announcements;

public class UpdateAnnouncementRequest
{
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public int? LinkedEventId { get; set; }
}
