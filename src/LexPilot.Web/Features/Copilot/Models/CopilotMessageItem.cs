namespace LexPilot.Web.Features.Copilot.Models;

public sealed class CopilotMessageItem
{
    public string Author { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
