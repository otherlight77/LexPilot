using LexPilot.Application.Clients;
using LexPilot.Domain.Clients;
using LexPilot.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ClientsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetAll(CancellationToken cancellationToken)
    {
        var clients = await _db.Clients
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Nom)
            .Select(x => new ClientDto(x.Id, x.Civilite, x.Nom, x.Prenom, x.Societe, x.Email, x.Telephone, x.Adresse))
            .ToListAsync(cancellationToken);

        return Ok(clients);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create(CreateClientRequest request, CancellationToken cancellationToken)
    {
        var client = new Client
        {
            Civilite = request.Civilite,
            Nom = request.Nom,
            Prenom = request.Prenom,
            Societe = request.Societe,
            Email = request.Email,
            Telephone = request.Telephone,
            Adresse = request.Adresse
        };

        _db.Clients.Add(client);
        await _db.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetAll), new { id = client.Id },
            new ClientDto(client.Id, client.Civilite, client.Nom, client.Prenom, client.Societe, client.Email, client.Telephone, client.Adresse));
    }
}
