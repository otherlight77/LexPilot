namespace LexPilot.AI.RAG;

public sealed class RagAnswer
{
    public string Answer { get; set; } = string.Empty;

    public List<RagCitation> Citations { get; set; } = [];

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
