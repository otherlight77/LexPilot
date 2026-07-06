namespace LexPilot.AI.RAG;

public interface IRagService
{
    Task<int> IndexAsync(RagIndexRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyList<RagCitation>> SearchAsync(RagSearchRequest request, CancellationToken cancellationToken);

    Task<RagAnswer> AskAsync(RagSearchRequest request, CancellationToken cancellationToken);

    Task<int> CountAsync(CancellationToken cancellationToken);

    Task ClearAsync(CancellationToken cancellationToken);
}
