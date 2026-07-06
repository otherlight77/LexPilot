using LexPilot.AI.Chat;

namespace LexPilot.AI.Providers;

public sealed class OllamaProvider : IAIProvider
{
    public string Name => "Ollama";

    public Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var response = new ChatResponse
        {
            
            Answer =
                "Provider Ollama pret mais non connecte. " +
                "Installe Ollama localement et configure le modele pour activer l'IA locale.",
            SuggestedActions =
            [
                "Installer Ollama",
                "Choisir un modele local",
                "Tester la connexion locale"
            ]
        };

        return Task.FromResult(response);
    }
}

