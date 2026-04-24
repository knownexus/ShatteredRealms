namespace ShatteredRealms.Application.DTOs.Announcements;

public class CreateAnnouncementRequest
{
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public int? LinkedEventId { get; set; }
}
