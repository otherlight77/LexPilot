using LexPilot.AI.DocumentEngine.Interfaces;
using LexPilot.AI.DocumentEngine.Models;

namespace LexPilot.AI.DocumentEngine.Extractors;

public sealed class PlainTextExtractor : IDocumentExtractor
{
    public bool CanHandle(string extension)
    {
        return extension.Equals(".txt", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".md", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".csv", StringComparison.OrdinalIgnoreCase);
    }

    public async Task<AnalyzedDocument> ExtractAsync(
        string file,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!File.Exists(file))
            throw new FileNotFoundException(file);

        var text = await File.ReadAllTextAsync(file, cancellationToken);

        return new AnalyzedDocument
        {
            Metadata =
            {
                FileName = Path.GetFileName(file),
                Extension = Path.GetExtension(file),
                Type = DocumentType.Text,
                Size = new FileInfo(file).Length
            },
            Content =
            {
                Text = text
            },
            ReadyForRag = !string.IsNullOrWhiteSpace(text)
        };
    }
}
