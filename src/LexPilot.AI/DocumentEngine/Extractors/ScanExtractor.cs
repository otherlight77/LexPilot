using LexPilot.AI.DocumentEngine.Interfaces;
using LexPilot.AI.DocumentEngine.Models;

namespace LexPilot.AI.DocumentEngine.Extractors;

public sealed class ScanExtractor : IDocumentExtractor
{
    private readonly ImageExtractor _imageExtractor;

    public ScanExtractor(ImageExtractor imageExtractor)
    {
        _imageExtractor = imageExtractor;
    }

    public bool CanHandle(string extension)
    {
        return _imageExtractor.CanHandle(extension);
    }

    public async Task<AnalyzedDocument> ExtractAsync(
        string file,
        CancellationToken cancellationToken)
    {
        var document = await _imageExtractor.ExtractAsync(file, cancellationToken);

        document.Metadata.Type = DocumentType.Scan;
        document.Metadata.Source = DocumentSource.Scanner;

        return document;
    }
}
