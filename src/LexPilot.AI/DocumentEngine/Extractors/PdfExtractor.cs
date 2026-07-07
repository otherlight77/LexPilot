using LexPilot.AI.DocumentEngine.Interfaces;
using LexPilot.AI.DocumentEngine.Models;

namespace LexPilot.AI.DocumentEngine.Extractors;

public sealed class PdfExtractor : IDocumentExtractor
{
    public bool CanHandle(string extension)
    {
        return extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase);
    }

    public async Task<AnalyzedDocument> ExtractAsync(
        string file,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var document = new AnalyzedDocument();

        document.Metadata.FileName = Path.GetFileName(file);
        document.Metadata.Extension = ".pdf";
        document.Metadata.Type = DocumentType.Pdf;

        if (!File.Exists(file))
            throw new FileNotFoundException(file);

        document.Metadata.Size = new FileInfo(file).Length;

        document.Content.Text =
            $"[PDF IMPORT]\n{document.Metadata.FileName}\n\nExtraction PDF en attente.";

        document.ReadyForRag = true;

        await Task.CompletedTask;

        return document;
    }
}
