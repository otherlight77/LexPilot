using MediatR;

namespace LexPilot.Application.Features.Clients.Commands.UpdateClient;

public sealed record UpdateClientCommand(
    Guid Id,
    string Nom,
    string Prenom,
    string? Email,
    string? Telephone
) : IRequest;
