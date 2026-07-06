using LexPilot.AI.Chunking;
using LexPilot.AI.Embeddings;
using LexPilot.AI.VectorStore;

namespace LexPilot.AI.RAG;

public sealed class RagService : IRagService
{
    private readonly ITextChunker _chunker;
    private readonly IEmbeddingService _embeddingService;
    private readonly IVectorStore _vectorStore;

    public RagService(
        ITextChunker chunker,
        IEmbeddingService embeddingService,
        IVectorStore vectorStore)
    {
        _chunker = chunker;
        _embeddingService = embeddingService;
        _vectorStore = vectorStore;
    }

    public async Task<int> IndexAsync(RagIndexRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Content))
            return 0;

        var chunks = _chunker.Split(request.SourceId, request.SourceName, request.Content);
        var count = 0;

        foreach (var chunk in chunks)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var embedding = await _embeddingService.EmbedAsync(chunk.Content, cancellationToken);

            await _vectorStore.UpsertAsync(new VectorDocument
            {
                Id = chunk.Id,
                SourceId = chunk.SourceId,
                SourceName = chunk.SourceName,
                Content = chunk.Content,
                ChunkIndex = chunk.Index,
                Embedding = embedding
            }, cancellationToken);

            count++;
        }

        return count;
    }

    public async Task<IReadOnlyList<RagCitation>> SearchAsync(RagSearchRequest request, CancellationToken cancellationToken)
    {
        var embedding = await _embeddingService.EmbedAsync(request.Query, cancellationToken);
        var results = await _vectorStore.SearchAsync(embedding, request.TopK, cancellationToken);

        return results
            .Select(x => new RagCitation
            {
                SourceId = x.SourceId,
                SourceName = x.SourceName,
                ChunkIndex = x.ChunkIndex,
                Score = Math.Round(x.Score, 4),
                Preview = x.Content.Length > 500 ? x.Content[..500] + "..." : x.Content
            })
            .ToList();
    }

    public async Task<RagAnswer> AskAsync(RagSearchRequest request, CancellationToken cancellationToken)
    {
        var citations = await SearchAsync(request, cancellationToken);

        var answer = citations.Count == 0
            ? "Aucune source pertinente trouvee dans l'index LexPilot."
            : BuildAnswer(request.Query, citations);

        return new RagAnswer
        {
            Answer = answer,
            Citations = citations.ToList()
        };
    }

    public Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return _vectorStore.CountAsync(cancellationToken);
    }

    public Task ClearAsync(CancellationToken cancellationToken)
    {
        return _vectorStore.ClearAsync(cancellationToken);
    }

    private static string BuildAnswer(string query, IReadOnlyList<RagCitation> citations)
    {
        var sources = string.Join(", ", citations.Select(x => $"{x.SourceName}#{x.ChunkIndex}"));

        return
            "Reponse RAG LexPilot basee sur les sources indexees." +
            Environment.NewLine +
            Environment.NewLine +
            "Question : " + query +
            Environment.NewLine +
            Environment.NewLine +
            "Sources utilisees : " + sources +
            Environment.NewLine +
            Environment.NewLine +
            "Synthese provisoire : les documents les plus proches ont ete retrouves. " +
            "Le prochain lot branchera cette recherche au Copilot pour generer une reponse juridique enrichie.";
    }
}
