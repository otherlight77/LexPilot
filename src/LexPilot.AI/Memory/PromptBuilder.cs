using LexPilot.AI.Chat;

namespace LexPilot.AI.Memory;

public sealed class PromptBuilder
{
    private readonly ContextBuilder _contextBuilder;

    public PromptBuilder(ContextBuilder contextBuilder)
    {
        _contextBuilder = contextBuilder;
    }

    public ChatRequest BuildRequestWithContext(ChatRequest request)
    {
        var context = _contextBuilder.BuildContext(request);

        if (!string.IsNullOrWhiteSpace(context))
        {
            request.Message =
                "Contexte LexPilot:" +
                Environment.NewLine +
                context +
                Environment.NewLine +
                Environment.NewLine +
                "Demande utilisateur:" +
                Environment.NewLine +
                request.Message;
        }

        return request;
    }
}
