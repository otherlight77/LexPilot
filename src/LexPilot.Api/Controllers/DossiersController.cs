using LexPilot.Application.Dossiers;
using LexPilot.Domain.Dossiers;
using LexPilot.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/dossiers")]
public class DossiersController : ControllerBase
{
    private readonly AppDbContext _db;

    public DossiersController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<DossierDto>>> GetAll(CancellationToken cancellationToken)
    {
        var dossiers = await _db.Dossiers
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new DossierDto(x.Id, x.Numero, x.Titre, x.Nature, x.Etat, x.Juridiction, x.ClientId))
            .ToListAsync(cancellationToken);

        return Ok(dossiers);
    }

    [HttpPost]
    public async Task<ActionResult<DossierDto>> Create(CreateDossierRequest request, CancellationToken cancellationToken)
    {
        var clientExists = await _db.Clients.AnyAsync(x => x.Id == request.ClientId && !x.IsDeleted, cancellationToken);
        if (!clientExists) return BadRequest("Client introuvable.");

        var dossier = new Dossier
        {
            Numero = request.Numero,
            Titre = request.Titre,
            Nature = request.Nature,
            Juridiction = request.Juridiction,
            ClientId = request.ClientId
        };

        _db.Dossiers.Add(dossier);
        await _db.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetAll), new { id = dossier.Id },
            new DossierDto(dossier.Id, dossier.Numero, dossier.Titre, dossier.Nature, dossier.Etat, dossier.Juridiction, dossier.ClientId));
    }
}
