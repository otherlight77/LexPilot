using LexPilot.AI.Chat;
using LexPilot.AI.Providers;

namespace LexPilot.AI.Services;

public sealed class CopilotService : ICopilotService
{
    private readonly IAIProviderFactory _providerFactory;

    public CopilotService(IAIProviderFactory providerFactory)
    {
        _providerFactory = providerFactory;
    }

    public async Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var provider = _providerFactory.GetProvider();

        var response = await provider.SendAsync(request, cancellationToken);

        if (string.IsNullOrWhiteSpace(response.Answer))
        {
            response.Answer = "Le provider IA n'a retourne aucune reponse.";
        }

        return response;
    }
}
