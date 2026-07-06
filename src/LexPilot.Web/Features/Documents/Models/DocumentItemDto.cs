namespace LexPilot.Web.Features.Documents.Models;

public sealed class DocumentItemDto
{
    public string FileName { get; set; } = string.Empty;
    public long Size { get; set; }
    public DateTime UploadedAt { get; set; }
    public string Extension { get; set; } = string.Empty;
}
