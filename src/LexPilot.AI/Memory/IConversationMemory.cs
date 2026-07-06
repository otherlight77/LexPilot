using LexPilot.AI.Chat;

namespace LexPilot.AI.Memory;

public interface IConversationMemory
{
    Conversation GetOrCreate(string? conversationId);

    void AddUserMessage(string conversationId, string message);

    void AddAssistantMessage(string conversationId, string message);

    IReadOnlyList<ChatMessage> GetHistory(string conversationId);
}
