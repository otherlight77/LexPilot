using LexPilot.AI.Chat;

namespace LexPilot.AI.Providers;

public sealed class FakeAIProvider : IAIProvider
{
    public string Name => "Fake";

    public Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var historyCount = request.History?.Count ?? 0;

        var response = new ChatResponse
        {
            ConversationId = request.ConversationId,
            Answer =
                "LexPilot Copilot fonctionne actuellement avec le provider Fake. " +
                $"Message recu : \"{request.Message}\". " +
                $"Historique disponible : {historyCount} message(s). " +
                "Les providers OpenAI, Azure OpenAI et Ollama sont prepares pour les prochains lots.",
            SuggestedActions =
            [
                "Resumer le dossier",
                "Analyser les documents",
                "Generer un courrier",
                "Verifier les echeances"
            ]
        };

        return Task.FromResult(response);
    }
}
