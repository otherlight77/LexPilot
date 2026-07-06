using Microsoft.AspNetCore.Mvc;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/documents")]
public sealed class DocumentsController : ControllerBase
{
    private readonly string _documentsRoot = @"C:\Microward\LexPilot\data\documents";

    [HttpGet]
    public IActionResult GetDocuments()
    {
        Directory.CreateDirectory(_documentsRoot);

        var files = Directory.GetFiles(_documentsRoot)
            .Select(file => new DocumentDto
            {
                FileName = Path.GetFileName(file),
                Size = new FileInfo(file).Length,
                UploadedAt = System.IO.File.GetCreationTimeUtc(file),
                Extension = Path.GetExtension(file)
            })
            .OrderByDescending(x => x.UploadedAt)
            .ToList();

        return Ok(files);
    }

    [HttpPost("upload")]
    [RequestSizeLimit(50_000_000)]
    public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
            return BadRequest("Aucun fichier recu.");

        Directory.CreateDirectory(_documentsRoot);

        var safeFileName = Path.GetFileName(file.FileName);
        var uniqueFileName = $"{DateTime.UtcNow:yyyyMMddHHmmss}_{safeFileName}";
        var path = Path.Combine(_documentsRoot, uniqueFileName);

        await using var stream = System.IO.File.Create(path);
        await file.CopyToAsync(stream, cancellationToken);

        return Ok(new
        {
            fileName = uniqueFileName,
            size = file.Length,
            status = "uploaded"
        });
    }

    public sealed class DocumentDto
    {
        public string FileName { get; set; } = string.Empty;
        public long Size { get; set; }
        public DateTime UploadedAt { get; set; }
        public string Extension { get; set; } = string.Empty;
    }
}
