using MediatR;

namespace LexPilot.Application.Features.Clients.Queries.GetClient;

public sealed record GetClientQuery(Guid Id) : IRequest<ClientDto?>;

public sealed record ClientDto(
    Guid Id,
    string Nom,
    string Prenom,
    string? Email,
    string? Telephone
);
