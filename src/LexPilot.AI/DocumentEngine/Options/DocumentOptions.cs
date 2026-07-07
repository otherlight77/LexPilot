namespace LexPilot.AI.DocumentEngine.Options;

public sealed class DocumentOptions
{
    public string ImportFolder { get; set; } = "C:\\Microward\\LexPilot\\imports";

    public string ProcessedFolder { get; set; } = "C:\\Microward\\LexPilot\\processed";

    public string FailedFolder { get; set; } = "C:\\Microward\\LexPilot\\failed";

    public bool AutoIndexInRag { get; set; } = true;

    public bool EnableOcr { get; set; } = true;

    public int MaxFileSizeMb { get; set; } = 50;

    public string[] AllowedExtensions { get; set; } =
    [
        ".pdf",
        ".doc",
        ".docx",
        ".txt",
        ".md",
        ".csv",
        ".png",
        ".jpg",
        ".jpeg",
        ".tif",
        ".tiff"
    ];
}
