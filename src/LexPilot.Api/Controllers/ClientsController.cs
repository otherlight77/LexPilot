using LexPilot.Application.Features.Clients.Commands.CreateClient;
using LexPilot.Application.Features.Clients.Commands.DeleteClient;
using LexPilot.Application.Features.Clients.Commands.UpdateClient;
using LexPilot.Application.Features.Clients.Queries.GetClient;
using LexPilot.Application.Features.Clients.Queries.GetClients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClientListItemDto>>> GetClients(CancellationToken cancellationToken)
    {
        var clients = await _mediator.Send(new GetClientsQuery(), cancellationToken);
        return Ok(clients);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDto>> GetClient(Guid id, CancellationToken cancellationToken)
    {
        var client = await _mediator.Send(new GetClientQuery(id), cancellationToken);

        if (client is null)
            return NotFound();

        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateClient(CreateClientCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetClient), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateClient(Guid id, UpdateClientCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("L'identifiant de l'URL ne correspond pas a l'identifiant du corps de la requete.");

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteClient(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteClientCommand(id), cancellationToken);
        return NoContent();
    }
}
