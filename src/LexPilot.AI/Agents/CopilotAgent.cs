using LexPilot.AI.Chat;

namespace LexPilot.AI.Agents;

public sealed class CopilotAgent
{
    public ChatResponse GenerateStubResponse(ChatRequest request)
    {
        var message = request.Message?.Trim();

        if (string.IsNullOrWhiteSpace(message))
        {
            return new ChatResponse
            {
                Answer = "Pose-moi une question sur un client, un dossier, un document ou une action a realiser.",
                SuggestedActions =
                [
                    "Resumer un dossier",
                    "Analyser un document",
                    "Generer un courrier"
                ]
            };
        }

        return new ChatResponse
        {
            Answer =
                "Je suis LexPilot Copilot. Pour le moment je fonctionne en mode simulation. " +
                $"J'ai bien recu ta demande : \"{message}\". " +
                "La prochaine etape consistera a connecter ce flux aux documents, dossiers, mails et au fournisseur IA.",
            SuggestedActions =
            [
                "Analyser les documents lies",
                "Preparer un projet de courrier",
                "Verifier les echeances",
                "Rechercher les informations du dossier"
            ]
        };
    }
}
