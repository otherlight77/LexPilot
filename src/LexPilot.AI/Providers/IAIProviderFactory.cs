namespace LexPilot.AI.Providers;

public interface IAIProviderFactory
{
    IAIProvider GetProvider();

    IAIProvider GetProvider(string? providerName);

    IReadOnlyList<string> GetAvailableProviders();
}
