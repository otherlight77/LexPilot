namespace LexPilot.AI.Chunking;

public interface ITextChunker
{
    IReadOnlyList<TextChunk> Split(string sourceId, string sourceName, string text, int maxChunkSize = 1200);
}
