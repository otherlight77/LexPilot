namespace LexPilot.AI.Embeddings;

public interface IEmbeddingService
{
    int Dimensions { get; }

    Task<float[]> EmbedAsync(string text, CancellationToken cancellationToken);
}
