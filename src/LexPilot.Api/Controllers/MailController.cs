using LexPilot.Infrastructure.Mail;
using Microsoft.AspNetCore.Mvc;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/mail")]
public class MailController : ControllerBase
{
    private readonly OvhMailService _mail;

    public MailController(OvhMailService mail)
    {
        _mail = mail;
    }

    [HttpGet("test-ovh")]
    public async Task<IActionResult> TestOvh()
    {
        return Ok(await _mail.TestConnectionAsync());
    }

    [HttpGet("inbox")]
    public async Task<IActionResult> Inbox([FromQuery] int max = 10)
    {
        return Ok(await _mail.GetLastInboxMessagesAsync(max));
    }

    [HttpPost("send-test")]
    public async Task<IActionResult> SendTest([FromQuery] string to)
    {
        await _mail.SendAsync(to, "Test LexPilot OVH", "Ceci est un test SMTP depuis LexPilot.");
        return Ok(new { sent = true, to });
    }
}
