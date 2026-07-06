using LexPilot.AI.Chat;

namespace LexPilot.AI.Providers;

public sealed class OpenAIProvider : IAIProvider
{
    public string Name => "OpenAI";

    public Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var response = new ChatResponse
        {
            
            Answer =
                "Provider OpenAI pret mais non connecte. " +
                "Ajoute la cle API et le modele dans la configuration pour activer ce provider.",
            SuggestedActions =
            [
                "Configurer OpenAI",
                "Choisir un modele",
                "Tester la connexion"
            ]
        };

        return Task.FromResult(response);
    }
}

