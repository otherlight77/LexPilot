namespace LexPilot.AI.DocumentEngine.Interfaces;

public interface IOcrEngine
{
    Task<string> ReadAsync(
        string imageFile,
        CancellationToken cancellationToken);
}
