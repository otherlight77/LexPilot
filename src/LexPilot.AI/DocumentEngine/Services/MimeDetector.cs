namespace LexPilot.AI.DocumentEngine.Services;

public sealed class MimeDetector
{
    public string Detect(string extension)
    {
        extension = extension.ToLowerInvariant();

        return extension switch
        {
            ".pdf" => "application/pdf",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".doc" => "application/msword",
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".txt" => "text/plain",
            ".md" => "text/markdown",
            ".csv" => "text/csv",
            _ => "application/octet-stream"
        };
    }
}
