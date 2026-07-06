namespace LexPilot.AI.Chat;

public sealed class ChatRequest
{
    public string? ConversationId { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? ClientContext { get; set; }

    public string? DossierContext { get; set; }

    public List<ChatMessage> History { get; set; } = [];
}
