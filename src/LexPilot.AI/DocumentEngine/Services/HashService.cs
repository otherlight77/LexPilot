using System.Security.Cryptography;

namespace LexPilot.AI.DocumentEngine.Services;

public sealed class HashService
{
    public string Compute(string file)
    {
        if (!File.Exists(file))
            throw new FileNotFoundException(file);

        using var sha = SHA256.Create();
        using var stream = File.OpenRead(file);

        var hash = sha.ComputeHash(stream);

        return Convert.ToHexString(hash);
    }
}
