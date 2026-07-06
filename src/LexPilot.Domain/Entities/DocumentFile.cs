namespace LexPilot.Domain.Entities;

public class DocumentFile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? ClientId { get; set; }
    public Guid? CaseFileId { get; set; }
    public string FileName { get; set; } = "";
    public string Category { get; set; } = "Document";
    public string StoragePath { get; set; } = "";
    public string? AiSummary { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
