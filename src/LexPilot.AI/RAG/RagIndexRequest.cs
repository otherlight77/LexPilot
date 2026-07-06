namespace LexPilot.AI.RAG;

public sealed class RagIndexRequest
{
    public string SourceId { get; set; } = Guid.NewGuid().ToString("N");

    public string SourceName { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}
