namespace LexPilot.AI.DocumentEngine.Models;

public sealed class DocumentContent
{
    public string Text { get; set; } = string.Empty;
    public List<string> Pages { get; set; } = [];
    public List<string> Images { get; set; } = [];
    public List<string> Tables { get; set; } = [];
    public List<string> Attachments { get; set; } = [];
}
