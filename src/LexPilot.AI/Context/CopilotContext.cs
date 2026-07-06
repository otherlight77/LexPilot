namespace LexPilot.AI.Context;

public sealed class CopilotContext
{
    public string UserName { get; set; } = "Utilisateur";
    public string? CurrentClient { get; set; }
    public string? CurrentDossier { get; set; }
    public int DocumentsCount { get; set; }
    public int MailsCount { get; set; }
    public int DeadlinesCount { get; set; }
}
