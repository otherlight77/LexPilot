using LexPilot.AI.Chat;
using LexPilot.AI.Configuration;
using Microsoft.Extensions.Options;

namespace LexPilot.AI.Providers;

public sealed class AnthropicProvider : BaseAIProvider
{
    private readonly AIProviderOptions _options;

    public AnthropicProvider(IOptions<AIProviderOptions> options)
    {
        _options = options.Value;
    }

    public override string Name => "Anthropic";

    public override Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ChatResponse
        {
            ConversationId = request.ConversationId,
            Answer =
                string.IsNullOrWhiteSpace(_options.AnthropicApiKey)
                    ? "Provider Anthropic pret mais cle API manquante."
                    : $"Provider Anthropic pret pour le modele {_options.AnthropicModel}. Integration HTTP a finaliser.",
            SuggestedActions = ["Configurer Anthropic", "Tester Claude", "Repasser en Fake"]
        });
    }
}
