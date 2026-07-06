using LexPilot.AI.Chat;

namespace LexPilot.AI.Providers;

public abstract class BaseAIProvider : IAIProvider
{
    public abstract string Name { get; }

    public virtual bool SupportsStreaming => true;

    public abstract Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken);

    public virtual async IAsyncEnumerable<string> StreamAsync(
        ChatRequest request,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var response = await SendAsync(request, cancellationToken);

        foreach (var word in response.Answer.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return word + " ";
            await Task.Delay(10, cancellationToken);
        }
    }
}
