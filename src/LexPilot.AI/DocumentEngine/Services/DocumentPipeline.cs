using LexPilot.AI.DocumentEngine.Extractors;
using LexPilot.AI.DocumentEngine.Interfaces;
using LexPilot.AI.DocumentEngine.Models;

namespace LexPilot.AI.DocumentEngine.Services;

public sealed class DocumentPipeline : IDocumentPipeline
{
    private readonly IReadOnlyList<IDocumentExtractor> _extractors;
    private readonly ITextCleaner _textCleaner;

    public DocumentPipeline(ITextCleaner textCleaner)
    {
        _textCleaner = textCleaner;

        _extractors = new List<IDocumentExtractor>
        {
            new PdfExtractor(),
            new WordExtractor(),
            new PlainTextExtractor()
        };
    }

    public async Task<AnalyzedDocument> AnalyzeAsync(
        string file,
        CancellationToken cancellationToken)
    {
        if (!File.Exists(file))
            throw new FileNotFoundException(file);

        var extension = Path.GetExtension(file);

        var extractor = _extractors.FirstOrDefault(x => x.CanHandle(extension));

        if (extractor is null)
            throw new NotSupportedException($"Extension {extension} non supportée.");

        var document = await extractor.ExtractAsync(file, cancellationToken);

        document.Content.Text = _textCleaner.Clean(document.Content.Text);

        document.ReadyForRag =
            !string.IsNullOrWhiteSpace(document.Content.Text);

        return document;
    }
}
