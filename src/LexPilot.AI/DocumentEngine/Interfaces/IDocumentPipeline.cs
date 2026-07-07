using LexPilot.AI.DocumentEngine.Models;

namespace LexPilot.AI.DocumentEngine.Interfaces;

public interface IDocumentPipeline
{
    Task<AnalyzedDocument> AnalyzeAsync(
        string file,
        CancellationToken cancellationToken);
}
