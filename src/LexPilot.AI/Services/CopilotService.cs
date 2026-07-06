using LexPilot.AI.Agents;
using LexPilot.AI.Chat;

namespace LexPilot.AI.Services;

public sealed class CopilotService : ICopilotService
{
    private readonly CopilotAgent _agent;

    public CopilotService(CopilotAgent agent)
    {
        _agent = agent;
    }

    public Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var response = _agent.GenerateStubResponse(request);
        return Task.FromResult(response);
    }
}
