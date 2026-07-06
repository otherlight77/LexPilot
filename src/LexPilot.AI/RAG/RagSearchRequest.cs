namespace LexPilot.AI.RAG;

public sealed class RagSearchRequest
{
    public string Query { get; set; } = string.Empty;

    public int TopK { get; set; } = 5;
}
