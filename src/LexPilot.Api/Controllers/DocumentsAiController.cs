using LexPilot.AI.DocumentEngine.Indexing;
using LexPilot.AI.DocumentEngine.Interfaces;
using LexPilot.AI.DocumentEngine.Models;
using LexPilot.AI.DocumentEngine.Queue;
using LexPilot.AI.DocumentEngine.Workers;
using Microsoft.AspNetCore.Mvc;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/documents-ai")]
public sealed class DocumentsAiController : ControllerBase
{
    private readonly IDocumentPipeline _documentPipeline;
    private readonly DocumentRagIndexer _ragIndexer;
    private readonly DocumentQueue _queue;
    private readonly FolderWatcherService _folderWatcher;
    private readonly BackgroundDocumentWorker _worker;
    private readonly IWebHostEnvironment _environment;

    public DocumentsAiController(
        IDocumentPipeline documentPipeline,
        DocumentRagIndexer ragIndexer,
        DocumentQueue queue,
        FolderWatcherService folderWatcher,
        BackgroundDocumentWorker worker,
        IWebHostEnvironment environment)
    {
        _documentPipeline = documentPipeline;
        _ragIndexer = ragIndexer;
        _queue = queue;
        _folderWatcher = folderWatcher;
        _worker = worker;
        _environment = environment;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<DocumentAnalysisResult>> Upload(
        IFormFile file,
        [FromForm] bool indexInRag = true,
        CancellationToken cancellationToken = default)
    {
        if (file.Length <= 0)
            return BadRequest("Fichier vide.");

        var importFolder = Path.Combine(_environment.ContentRootPath, "Storage", "Imports");
        Directory.CreateDirectory(importFolder);

        var safeName = Path.GetFileName(file.FileName);
        var storedName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{safeName}";
        var filePath = Path.Combine(importFolder, storedName);

        await using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        var document = await _documentPipeline.AnalyzeAsync(filePath, cancellationToken);

        var indexedChunks = indexInRag
            ? await _ragIndexer.IndexAsync(document, cancellationToken)
            : 0;

        return Ok(ToResult(document, indexedChunks));
    }

    [HttpPost("analyze-path")]
    public async Task<ActionResult<DocumentAnalysisResult>> AnalyzePath(
        [FromBody] AnalyzePathRequest request,
        CancellationToken cancellationToken)
    {
        var document = await _documentPipeline.AnalyzeAsync(request.FilePath, cancellationToken);

        var indexedChunks = request.IndexInRag
            ? await _ragIndexer.IndexAsync(document, cancellationToken)
            : 0;

        return Ok(ToResult(document, indexedChunks));
    }

    [HttpPost("queue")]
    public ActionResult<object> QueueDocument([FromBody] AnalyzePathRequest request)
    {
        _queue.Enqueue(new DocumentQueueItem
        {
            FilePath = request.FilePath,
            IndexInRag = request.IndexInRag
        });

        return Ok(new
        {
            queued = true,
            _queue.Count,
            request.FilePath
        });
    }

    [HttpPost("scan-import-folder")]
    public ActionResult<object> ScanImportFolder()
    {
        var added = _folderWatcher.ScanImportFolder();

        return Ok(new
        {
            added,
            queueCount = _queue.Count
        });
    }

    [HttpPost("process-queue")]
    public async Task<ActionResult<object>> ProcessQueue(CancellationToken cancellationToken)
    {
        var processed = await _worker.ProcessPendingAsync(cancellationToken);

        return Ok(new
        {
            processed,
            remaining = _queue.Count
        });
    }

    [HttpGet("queue-count")]
    public ActionResult<object> QueueCount()
    {
        return Ok(new
        {
            count = _queue.Count
        });
    }

    private static DocumentAnalysisResult ToResult(AnalyzedDocument document, int indexedChunks)
    {
        var preview = document.Content.Text;

        if (preview.Length > 800)
            preview = preview[..800] + "...";

        return new DocumentAnalysisResult
        {
            DocumentId = document.Metadata.Id,
            FileName = document.Metadata.FileName,
            Extension = document.Metadata.Extension,
            Type = document.Metadata.Type.ToString(),
            Size = document.Metadata.Size,
            Hash = document.Metadata.Hash,
            ReadyForRag = document.ReadyForRag,
            Indexed = document.Metadata.Indexed,
            IndexedChunks = indexedChunks,
            TextPreview = preview,
            ImportedUtc = document.Metadata.ImportedUtc
        };
    }
}

public sealed class AnalyzePathRequest
{
    public string FilePath { get; set; } = string.Empty;

    public bool IndexInRag { get; set; } = true;
}
