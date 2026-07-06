using LexPilot.AI.Chat;
using LexPilot.AI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/copilot")]
public sealed class CopilotController : ControllerBase
{
    private readonly ICopilotService _copilot;

    public CopilotController(ICopilotService copilot)
    {
        _copilot = copilot;
    }

    [HttpPost("chat")]
    public async Task<ActionResult<ChatResponse>> Chat(
        [FromBody] ChatRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
            return BadRequest("Le message est obligatoire.");

        var response = await _copilot.SendAsync(request, cancellationToken);

        return Ok(response);
    }
}
