namespace LexPilot.AI.Chat;

public sealed class ChatResponse
{
    public string Answer { get; set; } = string.Empty;
    public List<string> SuggestedActions { get; set; } = [];
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
