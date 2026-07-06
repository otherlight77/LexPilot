using LexPilot.AI.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/documents/analyze")]
public sealed class DocumentAnalysisController : ControllerBase
{
    private readonly IDocumentAnalysisPipeline _pipeline;
    private readonly string _documentsRoot = @"C:\Microward\LexPilot\data\documents";

    public DocumentAnalysisController(IDocumentAnalysisPipeline pipeline)
    {
        _pipeline = pipeline;
    }

    [HttpPost]
    public async Task<IActionResult> Analyze([FromBody] AnalyzeDocumentRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.FileName))
            return BadRequest("Nom de fichier obligatoire.");

        var safeFileName = Path.GetFileName(request.FileName);
        var path = Path.Combine(_documentsRoot, safeFileName);

        if (!System.IO.File.Exists(path))
            return NotFound("Document introuvable.");

        var result = await _pipeline.AnalyzeAsync(path, cancellationToken);

        return Ok(result);
    }

    public sealed class AnalyzeDocumentRequest
    {
        public string FileName { get; set; } = string.Empty;
    }
}
