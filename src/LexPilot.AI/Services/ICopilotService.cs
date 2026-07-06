using LexPilot.AI.Chat;

namespace LexPilot.AI.Services;

public interface ICopilotService
{
    Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken);
}
