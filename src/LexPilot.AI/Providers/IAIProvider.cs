using LexPilot.AI.Chat;

namespace LexPilot.AI.Providers;

public interface IAIProvider
{
    string Name { get; }

    bool SupportsStreaming { get; }

    Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken);

    IAsyncEnumerable<string> StreamAsync(ChatRequest request, CancellationToken cancellationToken);
}
