using LexPilot.AI.DocumentEngine.Interfaces;

namespace LexPilot.AI.DocumentEngine.Factory;

public sealed class DocumentExtractorFactory : IDocumentExtractorFactory
{
    private readonly IEnumerable<IDocumentExtractor> _extractors;

    public DocumentExtractorFactory(IEnumerable<IDocumentExtractor> extractors)
    {
        _extractors = extractors;
    }

    public IDocumentExtractor GetExtractor(string file)
    {
        var extension = Path.GetExtension(file);

        var extractor = _extractors.FirstOrDefault(x => x.CanHandle(extension));

        if (extractor is null)
            throw new NotSupportedException($"Aucun extracteur disponible pour l'extension {extension}.");

        return extractor;
    }
}
