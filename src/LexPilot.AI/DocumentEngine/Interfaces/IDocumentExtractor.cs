using LexPilot.AI.DocumentEngine.Models;

namespace LexPilot.AI.DocumentEngine.Interfaces;

public interface IDocumentExtractor
{
    bool CanHandle(string extension);

    Task<AnalyzedDocument> ExtractAsync(
        string file,
        CancellationToken cancellationToken);
}
