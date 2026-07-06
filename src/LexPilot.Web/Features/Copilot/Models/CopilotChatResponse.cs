namespace LexPilot.Web.Features.Copilot.Models;

public sealed class CopilotChatResponse
{
    public string Answer { get; set; } = string.Empty;
    public List<string> SuggestedActions { get; set; } = [];
    public DateTime CreatedAtUtc { get; set; }
}
