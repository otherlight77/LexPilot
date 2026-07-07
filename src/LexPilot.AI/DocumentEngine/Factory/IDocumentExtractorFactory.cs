using LexPilot.AI.DocumentEngine.Interfaces;

namespace LexPilot.AI.DocumentEngine.Factory;

public interface IDocumentExtractorFactory
{
    IDocumentExtractor GetExtractor(string file);
}
