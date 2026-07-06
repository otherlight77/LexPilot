namespace LexPilot.AI.VectorStore;

public interface IVectorStore
{
    Task UpsertAsync(VectorDocument document, CancellationToken cancellationToken);

    Task<IReadOnlyList<VectorSearchResult>> SearchAsync(float[] queryEmbedding, int topK, CancellationToken cancellationToken);

    Task ClearAsync(CancellationToken cancellationToken);

    Task<int> CountAsync(CancellationToken cancellationToken);
}
