namespace LexPilot.AI.VectorStore;

public sealed class VectorSearchResult
{
    public string Id { get; set; } = string.Empty;

    public string SourceId { get; set; } = string.Empty;

    public string SourceName { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public int ChunkIndex { get; set; }

    public double Score { get; set; }
}
