using LexPilot.AI.DocumentEngine.Interfaces;
using LexPilot.AI.DocumentEngine.Models;
using LexPilot.AI.RAG;

namespace LexPilot.AI.DocumentEngine.Indexing;

public sealed class DocumentRagIndexer
{
    private readonly IRagService _ragService;

    public DocumentRagIndexer(IRagService ragService)
    {
        _ragService = ragService;
    }

    public async Task<int> IndexAsync(
        AnalyzedDocument document,
        CancellationToken cancellationToken)
    {
        if (!document.ReadyForRag || string.IsNullOrWhiteSpace(document.Content.Text))
            return 0;

        var request = new RagIndexRequest
        {
            SourceId = document.Metadata.Id.ToString("N"),
            SourceName = document.Metadata.FileName,
            Content = document.Content.Text
        };

        var count = await _ragService.IndexAsync(request, cancellationToken);

        document.Metadata.Indexed = count > 0;

        return count;
    }
}
