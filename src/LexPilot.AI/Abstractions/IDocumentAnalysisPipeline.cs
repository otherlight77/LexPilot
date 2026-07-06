using LexPilot.AI.Models;

namespace LexPilot.AI.Abstractions;

public interface IDocumentAnalysisPipeline
{
    Task<DocumentAnalysisResult> AnalyzeAsync(string filePath, CancellationToken cancellationToken);
}
