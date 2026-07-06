using LexPilot.Domain.Entities;
using LexPilot.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/cases")]
public class CasesController : ControllerBase
{
    private readonly LexPilotDbContext _db;

    public CasesController(LexPilotDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _db.CaseFiles.OrderByDescending(x => x.CreatedAtUtc).ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CaseFile caseFile)
    {
        if (string.IsNullOrWhiteSpace(caseFile.Reference))
            caseFile.Reference = "LEX-" + DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");

        _db.CaseFiles.Add(caseFile);
        await _db.SaveChangesAsync();
        return Ok(caseFile);
    }
}
