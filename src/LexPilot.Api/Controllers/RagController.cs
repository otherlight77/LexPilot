using LexPilot.AI.RAG;
using Microsoft.AspNetCore.Mvc;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/rag")]
public sealed class RagController : ControllerBase
{
    private readonly IRagService _ragService;

    public RagController(IRagService ragService)
    {
        _ragService = ragService;
    }

    [HttpPost("index")]
    public async Task<ActionResult<object>> Index(
        [FromBody] RagIndexRequest request,
        CancellationToken cancellationToken)
    {
        var count = await _ragService.IndexAsync(request, cancellationToken);

        return Ok(new
        {
            indexedChunks = count,
            request.SourceId,
            request.SourceName
        });
    }

    [HttpPost("search")]
    public async Task<ActionResult<IReadOnlyList<RagCitation>>> Search(
        [FromBody] RagSearchRequest request,
        CancellationToken cancellationToken)
    {
        var results = await _ragService.SearchAsync(request, cancellationToken);
        return Ok(results);
    }

    [HttpPost("ask")]
    public async Task<ActionResult<RagAnswer>> Ask(
        [FromBody] RagSearchRequest request,
        CancellationToken cancellationToken)
    {
        var answer = await _ragService.AskAsync(request, cancellationToken);
        return Ok(answer);
    }

    [HttpGet("count")]
    public async Task<ActionResult<object>> Count(CancellationToken cancellationToken)
    {
        var count = await _ragService.CountAsync(cancellationToken);
        return Ok(new { count });
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> Clear(CancellationToken cancellationToken)
    {
        await _ragService.ClearAsync(cancellationToken);
        return NoContent();
    }
}
