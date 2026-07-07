namespace LexPilot.AI.DocumentEngine.Models;

public sealed class AnalyzedDocument
{
    public DocumentMetadata Metadata { get; set; } = new();
    public DocumentContent Content { get; set; } = new();
    public List<string> Keywords { get; set; } = [];
    public List<DateTime> Dates { get; set; } = [];
    public List<string> Persons { get; set; } = [];
    public List<string> Companies { get; set; } = [];
    public List<string> Addresses { get; set; } = [];
    public List<string> Emails { get; set; } = [];
    public List<string> Phones { get; set; } = [];
    public string Summary { get; set; } = string.Empty;
    public bool ReadyForRag { get; set; }
}
