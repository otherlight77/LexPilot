namespace LexPilot.AI.DocumentEngine.Models;

public sealed class DocumentAnalysisResult
{
    public Guid DocumentId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string Extension { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public long Size { get; set; }

    public string Hash { get; set; } = string.Empty;

    public bool ReadyForRag { get; set; }

    public bool Indexed { get; set; }

    public int IndexedChunks { get; set; }

    public string TextPreview { get; set; } = string.Empty;

    public DateTime ImportedUtc { get; set; } = DateTime.UtcNow;
}
