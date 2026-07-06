using MediatR;

namespace LexPilot.Application.Features.Clients.Commands.CreateClient;

public sealed record CreateClientCommand(
    string Nom,
    string Prenom,
    string? Email,
    string? Telephone
) : IRequest<Guid>;
