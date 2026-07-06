using System.Collections.Concurrent;

namespace LexPilot.AI.VectorStore;

public sealed class InMemoryVectorStore : IVectorStore
{
    private readonly ConcurrentDictionary<string, VectorDocument> _documents = new();

    public Task UpsertAsync(VectorDocument document, CancellationToken cancellationToken)
    {
        _documents[document.Id] = document;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<VectorSearchResult>> SearchAsync(float[] queryEmbedding, int topK, CancellationToken cancellationToken)
    {
        var results = _documents.Values
            .Select(document => new VectorSearchResult
            {
                Id = document.Id,
                SourceId = document.SourceId,
                SourceName = document.SourceName,
                Content = document.Content,
                ChunkIndex = document.ChunkIndex,
                Score = CosineSimilarity(queryEmbedding, document.Embedding)
            })
            .OrderByDescending(x => x.Score)
            .Take(topK)
            .ToList();

        return Task.FromResult<IReadOnlyList<VectorSearchResult>>(results);
    }

    public Task ClearAsync(CancellationToken cancellationToken)
    {
        _documents.Clear();
        return Task.CompletedTask;
    }

    public Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_documents.Count);
    }

    private static double CosineSimilarity(float[] a, float[] b)
    {
        if (a.Length == 0 || b.Length == 0 || a.Length != b.Length)
            return 0;

        double dot = 0;
        double normA = 0;
        double normB = 0;

        for (var i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            normA += a[i] * a[i];
            normB += b[i] * b[i];
        }

        if (normA <= 0 || normB <= 0)
            return 0;

        return dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
    }
}
