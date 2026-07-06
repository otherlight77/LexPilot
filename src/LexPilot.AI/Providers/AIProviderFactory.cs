using LexPilot.AI.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LexPilot.AI.Providers;

public sealed class AIProviderFactory : IAIProviderFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AIProviderOptions _options;

    public AIProviderFactory(IServiceProvider serviceProvider, IOptions<AIProviderOptions> options)
    {
        _serviceProvider = serviceProvider;
        _options = options.Value;
    }

    public IAIProvider GetProvider()
    {
        return GetProvider(_options.Provider);
    }

    public IAIProvider GetProvider(string? providerName)
    {
        return (providerName ?? "Fake").Trim().ToLowerInvariant() switch
        {
            "openai" => _serviceProvider.GetRequiredService<OpenAIProvider>(),
            "azureopenai" => _serviceProvider.GetRequiredService<AzureOpenAIProvider>(),
            "azure" => _serviceProvider.GetRequiredService<AzureOpenAIProvider>(),
            "ollama" => _serviceProvider.GetRequiredService<OllamaProvider>(),
            "anthropic" => _serviceProvider.GetRequiredService<AnthropicProvider>(),
            "claude" => _serviceProvider.GetRequiredService<AnthropicProvider>(),
            "gemini" => _serviceProvider.GetRequiredService<GeminiProvider>(),
            "google" => _serviceProvider.GetRequiredService<GeminiProvider>(),
            _ => _serviceProvider.GetRequiredService<FakeAIProvider>()
        };
    }

    public IReadOnlyList<string> GetAvailableProviders()
    {
        return
        [
            "Fake",
            "OpenAI",
            "AzureOpenAI",
            "Ollama",
            "Anthropic",
            "Gemini"
        ];
    }
}
