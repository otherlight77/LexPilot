using System.Security.Cryptography;
using System.Text;

namespace LexPilot.AI.Embeddings;

public sealed class DeterministicEmbeddingService : IEmbeddingService
{
    public int Dimensions => 128;

    public Task<float[]> EmbedAsync(string text, CancellationToken cancellationToken)
    {
        var vector = new float[Dimensions];

        if (string.IsNullOrWhiteSpace(text))
            return Task.FromResult(vector);

        var words = text
            .ToLowerInvariant()
            .Split([' ', '\r', '\n', '\t', '.', ',', ';', ':', '!', '?', '(', ')', '[', ']', '{', '}', '"', '\''], StringSplitOptions.RemoveEmptyEntries);

        foreach (var word in words)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(word));
            var index = BitConverter.ToUInt16(hash, 0) % Dimensions;
            vector[index] += 1f;
        }

        Normalize(vector);

        return Task.FromResult(vector);
    }

    private static void Normalize(float[] vector)
    {
        var sum = 0d;

        foreach (var value in vector)
            sum += value * value;

        var length = Math.Sqrt(sum);

        if (length <= 0)
            return;

        for (var i = 0; i < vector.Length; i++)
            vector[i] = (float)(vector[i] / length);
    }
}
