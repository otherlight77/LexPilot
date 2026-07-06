using Microsoft.AspNetCore.Mvc;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Status = "Healthy",
            Api = "OK",
            Database = "Pending check",
            Microsoft365 = "Not configured",
            AI = "Not configured",
            Date = DateTime.UtcNow
        });
    }
}
