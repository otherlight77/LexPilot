namespace LexPilot.AI.Models;

public sealed class DocumentAnalysisResult
{
    public string FileName { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public string SuggestedClient { get; set; } = string.Empty;
    public string SuggestedDossier { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public decimal Confidence { get; set; }
    public List<string> DetectedDates { get; set; } = [];
    public List<string> DetectedAmounts { get; set; } = [];
    public List<ActionSuggestion> SuggestedActions { get; set; } = [];
}
