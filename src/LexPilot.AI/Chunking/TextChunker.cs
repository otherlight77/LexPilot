namespace LexPilot.AI.Chunking;

public sealed class TextChunker : ITextChunker
{
    public IReadOnlyList<TextChunk> Split(string sourceId, string sourceName, string text, int maxChunkSize = 1200)
    {
        var chunks = new List<TextChunk>();

        if (string.IsNullOrWhiteSpace(text))
            return chunks;

        var normalized = text.Replace("\r\n", "\n").Trim();
        var index = 0;
        var position = 0;

        while (position < normalized.Length)
        {
            var remaining = normalized.Length - position;
            var size = Math.Min(maxChunkSize, remaining);
            var content = normalized.Substring(position, size).Trim();

            if (!string.IsNullOrWhiteSpace(content))
            {
                chunks.Add(new TextChunk
                {
                    SourceId = sourceId,
                    SourceName = sourceName,
                    Content = content,
                    Index = index
                });

                index++;
            }

            position += size;
        }

        return chunks;
    }
}
