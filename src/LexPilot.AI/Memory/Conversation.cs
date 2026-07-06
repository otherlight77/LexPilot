using LexPilot.AI.Chat;

namespace LexPilot.AI.Memory;

public sealed class Conversation
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    public List<ChatMessage> Messages { get; set; } = [];

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}
