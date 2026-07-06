using LexPilot.AI.Chat;

namespace LexPilot.AI.Providers;

public sealed class AzureOpenAIProvider : IAIProvider
{
    public string Name => "AzureOpenAI";

    public Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var response = new ChatResponse
        {
            
            Answer =
                "Provider Azure OpenAI pret mais non connecte. " +
                "Ajoute endpoint, cle API et deployment dans la configuration pour activer ce provider.",
            SuggestedActions =
            [
                "Configurer Azure OpenAI",
                "Verifier le deployment",
                "Tester la connexion"
            ]
        };

        return Task.FromResult(response);
    }
}

