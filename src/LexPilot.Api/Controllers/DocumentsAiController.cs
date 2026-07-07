using LexPilot.AI.DocumentEngine.Indexing;
using LexPilot.AI.DocumentEngine.Interfaces;
using LexPilot.AI.DocumentEngine.Models;
using Microsoft.AspNetCore.Mvc;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/documents-ai")]
public sealed class DocumentsAiController : ControllerBase
{
    private readonly IDocumentPipeline _documentPipeline;
    private readonly DocumentRagIndexer _ragIndexer;

    public DocumentsAiController(
        IDocumentPipeline documentPipeline,
        DocumentRagIndexer ragIndexer)
    {
        _documentPipeline = documentPipeline;
        _ragIndexer = ragIndexer;
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

        var preview = document.Content.Text;

        if (preview.Length > 800)
            preview = preview[..800] + "...";

        return Ok(new DocumentAnalysisResult
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
        });
    }
}

public sealed class AnalyzePathRequest
{
    public string FilePath { get; set; } = string.Empty;

    public bool IndexInRag { get; set; } = true;
}
