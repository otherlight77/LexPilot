using MediatR;

namespace LexPilot.Application.Features.Clients.Queries.GetClients;

public sealed record GetClientsQuery : IRequest<IReadOnlyList<ClientListItemDto>>;

public sealed record ClientListItemDto(
    Guid Id,
    string Nom,
    string Prenom,
    string? Email,
    string? Telephone
);
