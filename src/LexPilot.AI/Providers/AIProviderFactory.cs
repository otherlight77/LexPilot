using LexPilot.AI.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LexPilot.AI.Providers;

public sealed class AIProviderFactory : IAIProviderFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AIProviderOptions _options;

    public AIProviderFactory(
        IServiceProvider serviceProvider,
        IOptions<AIProviderOptions> options)
    {
        _serviceProvider = serviceProvider;
        _options = options.Value;
    }

    public IAIProvider GetProvider()
    {
        return (_options.Provider ?? "Fake").Trim().ToLowerInvariant() switch
        {
            "openai" => _serviceProvider.GetRequiredService<OpenAIProvider>(),
            "azureopenai" => _serviceProvider.GetRequiredService<AzureOpenAIProvider>(),
            "azure" => _serviceProvider.GetRequiredService<AzureOpenAIProvider>(),
            "ollama" => _serviceProvider.GetRequiredService<OllamaProvider>(),
            _ => _serviceProvider.GetRequiredService<FakeAIProvider>()
        };
    }
}
