namespace LexPilot.AI.Models;

public sealed class ActionSuggestion
{
    public string Label { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsRecommended { get; set; }
}
