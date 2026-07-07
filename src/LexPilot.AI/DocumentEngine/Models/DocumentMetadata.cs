namespace LexPilot.AI.DocumentEngine.Models;

public sealed class DocumentMetadata
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public long Size { get; set; }
    public DocumentType Type { get; set; } = DocumentType.Unknown;
    public DocumentSource Source { get; set; } = DocumentSource.API;
    public DateTime ImportedUtc { get; set; } = DateTime.UtcNow;
    public string MimeType { get; set; } = string.Empty;
    public string Hash { get; set; } = string.Empty;
    public int Pages { get; set; }
    public string Language { get; set; } = string.Empty;
    public bool OCRApplied { get; set; }
    public bool Indexed { get; set; }
    public Dictionary<string, string> Properties { get; set; } = [];
}
