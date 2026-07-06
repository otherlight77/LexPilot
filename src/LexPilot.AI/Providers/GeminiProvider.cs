using LexPilot.AI.Chat;
using LexPilot.AI.Configuration;
using Microsoft.Extensions.Options;

namespace LexPilot.AI.Providers;

public sealed class GeminiProvider : BaseAIProvider
{
    private readonly AIProviderOptions _options;

    public GeminiProvider(IOptions<AIProviderOptions> options)
    {
        _options = options.Value;
    }

    public override string Name => "Gemini";

    public override Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ChatResponse
        {
            ConversationId = request.ConversationId,
            Answer =
                string.IsNullOrWhiteSpace(_options.GeminiApiKey)
                    ? "Provider Gemini pret mais cle API manquante."
                    : $"Provider Gemini pret pour le modele {_options.GeminiModel}. Integration HTTP a finaliser.",
            SuggestedActions = ["Configurer Gemini", "Tester Gemini", "Repasser en Fake"]
        });
    }
}
