using LexPilot.AI.Chat;
using LexPilot.AI.Memory;
using LexPilot.AI.Providers;

namespace LexPilot.AI.Services;

public sealed class CopilotService : ICopilotService
{
    private readonly IAIProviderFactory _providerFactory;
    private readonly IConversationMemory _memory;
    private readonly PromptBuilder _promptBuilder;

    public CopilotService(
        IAIProviderFactory providerFactory,
        IConversationMemory memory,
        PromptBuilder promptBuilder)
    {
        _providerFactory = providerFactory;
        _memory = memory;
        _promptBuilder = promptBuilder;
    }

    public async Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var conversation = _memory.GetOrCreate(request.ConversationId);

        _memory.AddUserMessage(conversation.Id, request.Message);

        request.ConversationId = conversation.Id;
        request.History = _memory.GetHistory(conversation.Id).ToList();

        var contextualRequest = _promptBuilder.BuildRequestWithContext(request);

        var provider = _providerFactory.GetProvider();

        var response = await provider.SendAsync(contextualRequest, cancellationToken);

        if (string.IsNullOrWhiteSpace(response.Answer))
        {
            response.Answer = "Le provider IA n'a retourne aucune reponse.";
        }

        response.ConversationId = conversation.Id;

        _memory.AddAssistantMessage(conversation.Id, response.Answer);

        return response;
    }
}
