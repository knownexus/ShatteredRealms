namespace ShatteredRealms.Application.DTOs.Documents;

public class DocumentDto
{
    public int Id { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public string UploadedById { get; set; } = string.Empty;
    public string UploadedByName { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}
