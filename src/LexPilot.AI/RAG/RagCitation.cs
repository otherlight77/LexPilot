namespace LexPilot.AI.RAG;

public sealed class RagCitation
{
    public string SourceId { get; set; } = string.Empty;

    public string SourceName { get; set; } = string.Empty;

    public int ChunkIndex { get; set; }

    public double Score { get; set; }

    public string Preview { get; set; } = string.Empty;
}
