namespace LexPilot.AI.Chunking;

public sealed class TextChunk
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    public string SourceId { get; set; } = string.Empty;

    public string SourceName { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public int Index { get; set; }
}
