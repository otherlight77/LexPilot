using LexPilot.AI.DocumentEngine.Interfaces;
using LexPilot.AI.DocumentEngine.Models;

namespace LexPilot.AI.DocumentEngine.Services;

public interface IMetadataExtractor
{
    Task<DocumentMetadata> ReadAsync(string file, CancellationToken cancellationToken);
}

public sealed class MetadataExtractor : IMetadataExtractor
{
    private readonly MimeDetector _mimeDetector;
    private readonly HashService _hashService;

    public MetadataExtractor(
        MimeDetector mimeDetector,
        HashService hashService)
    {
        _mimeDetector = mimeDetector;
        _hashService = hashService;
    }

    public Task<DocumentMetadata> ReadAsync(string file, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!File.Exists(file))
            throw new FileNotFoundException(file);

        var extension = Path.GetExtension(file);

        var metadata = new DocumentMetadata
        {
            FileName = Path.GetFileName(file),
            Extension = extension,
            Size = new FileInfo(file).Length,
            MimeType = _mimeDetector.Detect(extension),
            Hash = _hashService.Compute(file),
            ImportedUtc = DateTime.UtcNow,
            Source = DocumentSource.API,
            Type = extension.ToLowerInvariant() switch
            {
                ".pdf" => DocumentType.Pdf,
                ".doc" => DocumentType.Word,
                ".docx" => DocumentType.Word,
                ".png" => DocumentType.Image,
                ".jpg" => DocumentType.Image,
                ".jpeg" => DocumentType.Image,
                ".txt" => DocumentType.Text,
                ".md" => DocumentType.Text,
                ".csv" => DocumentType.Text,
                _ => DocumentType.Unknown
            }
        };

        return Task.FromResult(metadata);
    }
}
