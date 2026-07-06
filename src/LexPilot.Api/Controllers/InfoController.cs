using Microsoft.AspNetCore.Mvc;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/info")]
public sealed class InfoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Application = "LexPilot AI",
            Version = "1.0.0-dev",
            Lot = "1.1.4",
            Status = "Running",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
            Date = DateTime.UtcNow
        });
    }
}
