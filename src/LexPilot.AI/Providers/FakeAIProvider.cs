using LexPilot.AI.Chat;

namespace LexPilot.AI.Providers;

public sealed class FakeAIProvider : BaseAIProvider
{
    public override string Name => "Fake";

    public override Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var historyCount = request.History?.Count ?? 0;

        return Task.FromResult(new ChatResponse
        {
            ConversationId = request.ConversationId,
            Answer =
                "LexPilot Copilot fonctionne avec le provider Fake. " +
                $"Message recu : \"{request.Message}\". " +
                $"Historique disponible : {historyCount} message(s).",
            SuggestedActions =
            [
                "Resumer le dossier",
                "Analyser les documents",
                "Generer un courrier",
                "Verifier les echeances"
            ]
        });
    }
}
