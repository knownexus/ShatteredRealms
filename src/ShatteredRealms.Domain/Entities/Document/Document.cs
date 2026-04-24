namespace ShatteredRealms.Domain.Entities.Document;

public class Document
{
    public int Id { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public string UploadedById { get; set; } = string.Empty;
    public User.User UploadedBy { get; set; } = null!;
    public DateTime UploadedAt { get; set; }
    public bool IsDeleted { get; set; }
}
