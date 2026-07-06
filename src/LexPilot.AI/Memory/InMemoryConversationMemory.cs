using LexPilot.AI.Chat;
using System.Collections.Concurrent;

namespace LexPilot.AI.Memory;

public sealed class InMemoryConversationMemory : IConversationMemory
{
    private readonly ConcurrentDictionary<string, Conversation> _conversations = new();

    public Conversation GetOrCreate(string? conversationId)
    {
        if (!string.IsNullOrWhiteSpace(conversationId) &&
            _conversations.TryGetValue(conversationId, out var existing))
        {
            return existing;
        }

        var conversation = new Conversation();
        _conversations[conversation.Id] = conversation;

        return conversation;
    }

    public void AddUserMessage(string conversationId, string message)
    {
        AddMessage(conversationId, ChatRole.User, message);
    }

    public void AddAssistantMessage(string conversationId, string message)
    {
        AddMessage(conversationId, ChatRole.Assistant, message);
    }

    public IReadOnlyList<ChatMessage> GetHistory(string conversationId)
    {
        return _conversations.TryGetValue(conversationId, out var conversation)
            ? conversation.Messages
            : [];
    }

    private void AddMessage(string conversationId, ChatRole role, string message)
    {
        var conversation = GetOrCreate(conversationId);

        conversation.Messages.Add(new ChatMessage
        {
            Role = role,
            Content = message,
            CreatedAtUtc = DateTime.UtcNow
        });

        conversation.UpdatedAtUtc = DateTime.UtcNow;
    }
}
