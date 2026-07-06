using LexPilot.AI.Chat;

namespace LexPilot.AI.Memory;

public sealed class ContextBuilder
{
    public string BuildContext(ChatRequest request)
    {
        var parts = new List<string>();

        if (!string.IsNullOrWhiteSpace(request.ClientContext))
            parts.Add("Client: " + request.ClientContext);

        if (!string.IsNullOrWhiteSpace(request.DossierContext))
            parts.Add("Dossier: " + request.DossierContext);

        if (request.History.Count > 0)
            parts.Add("Historique: " + request.History.Count + " message(s)");

        return string.Join(Environment.NewLine, parts);
    }
}
