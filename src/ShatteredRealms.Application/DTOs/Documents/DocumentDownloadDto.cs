namespace ShatteredRealms.Application.DTOs.Documents;

public class DocumentDownloadDto
{
    public string StoredFileName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
}
