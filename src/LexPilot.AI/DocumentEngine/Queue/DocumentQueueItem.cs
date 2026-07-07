namespace LexPilot.AI.DocumentEngine.Queue;

public sealed class DocumentQueueItem
{
    public string FilePath { get; set; } = string.Empty;

    public bool IndexInRag { get; set; } = true;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
