using LexPilot.AI.DocumentEngine.Interfaces;
using LexPilot.AI.DocumentEngine.Models;

namespace LexPilot.AI.DocumentEngine.Extractors;

public sealed class WordExtractor : IDocumentExtractor
{
    public bool CanHandle(string extension)
    {
        return extension.Equals(".docx", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".doc", StringComparison.OrdinalIgnoreCase);
    }

    public async Task<AnalyzedDocument> ExtractAsync(
        string file,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!File.Exists(file))
            throw new FileNotFoundException(file);

        var document = new AnalyzedDocument();

        document.Metadata.FileName = Path.GetFileName(file);
        document.Metadata.Extension = Path.GetExtension(file);
        document.Metadata.Type = DocumentType.Word;
        document.Metadata.Size = new FileInfo(file).Length;

        document.Content.Text =
            $"[WORD IMPORT]\n{document.Metadata.FileName}\n\nExtraction Word en attente.";

        document.ReadyForRag = true;

        await Task.CompletedTask;

        return document;
    }
}
