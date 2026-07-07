using LexPilot.AI.DocumentEngine.Indexing;
using LexPilot.AI.DocumentEngine.Interfaces;
using LexPilot.AI.DocumentEngine.Queue;

namespace LexPilot.AI.DocumentEngine.Workers;

public sealed class BackgroundDocumentWorker
{
    private readonly DocumentQueue _queue;
    private readonly IDocumentPipeline _pipeline;
    private readonly DocumentRagIndexer _indexer;

    public BackgroundDocumentWorker(
        DocumentQueue queue,
        IDocumentPipeline pipeline,
        DocumentRagIndexer indexer)
    {
        _queue = queue;
        _pipeline = pipeline;
        _indexer = indexer;
    }

    public async Task<int> ProcessPendingAsync(CancellationToken cancellationToken)
    {
        var processed = 0;

        while (_queue.TryDequeue(out var item) && item is not null)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var document = await _pipeline.AnalyzeAsync(item.FilePath, cancellationToken);

            if (item.IndexInRag)
                await _indexer.IndexAsync(document, cancellationToken);

            processed++;
        }

        return processed;
    }
}
