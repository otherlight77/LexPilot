namespace LexPilot.AI.VectorStore;

public sealed class VectorDocument
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    public string SourceId { get; set; } = string.Empty;

    public string SourceName { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public int ChunkIndex { get; set; }

    public float[] Embedding { get; set; } = [];

    public DateTime IndexedAtUtc { get; set; } = DateTime.UtcNow;
}
