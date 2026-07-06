using LexPilot.AI.Chat;

namespace LexPilot.AI.Providers;

public interface IAIProvider
{
    string Name { get; }

    Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken);
}
