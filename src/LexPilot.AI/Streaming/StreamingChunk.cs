namespace LexPilot.AI.Streaming;

public sealed class StreamingChunk
{
    public string Content { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
