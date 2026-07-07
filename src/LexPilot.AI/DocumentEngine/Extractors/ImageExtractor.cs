using LexPilot.AI.DocumentEngine.Interfaces;
using LexPilot.AI.DocumentEngine.Models;

namespace LexPilot.AI.DocumentEngine.Extractors;

public sealed class ImageExtractor : IDocumentExtractor
{
    private readonly IOcrEngine _ocrEngine;

    public ImageExtractor(IOcrEngine ocrEngine)
    {
        _ocrEngine = ocrEngine;
    }

    public bool CanHandle(string extension)
    {
        return extension.Equals(".png", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".tif", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".tiff", StringComparison.OrdinalIgnoreCase);
    }

    public async Task<AnalyzedDocument> ExtractAsync(
        string file,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!File.Exists(file))
            throw new FileNotFoundException(file);

        var ocrText = await _ocrEngine.ReadAsync(file, cancellationToken);

        return new AnalyzedDocument
        {
            Metadata =
            {
                FileName = Path.GetFileName(file),
                Extension = Path.GetExtension(file),
                Type = DocumentType.Image,
                Size = new FileInfo(file).Length,
                OCRApplied = true
            },
            Content =
            {
                Text = ocrText,
                Images = [file]
            },
            ReadyForRag = !string.IsNullOrWhiteSpace(ocrText)
        };
    }
}
