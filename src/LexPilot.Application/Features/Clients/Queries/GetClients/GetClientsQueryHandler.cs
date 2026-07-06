using LexPilot.Application.Common.Interfaces;
using MediatR;

namespace LexPilot.Application.Features.Clients.Queries.GetClients;

public sealed class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IReadOnlyList<ClientListItemDto>>
{
    private readonly IApplicationDbContext _context;

    public GetClientsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IReadOnlyList<ClientListItemDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = _context.Clients
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Nom)
            .ThenBy(x => x.Prenom)
            .Select(x => new ClientListItemDto(
                x.Id,
                x.Nom,
                x.Prenom,
                x.Email,
                x.Telephone))
            .ToList();

        return Task.FromResult<IReadOnlyList<ClientListItemDto>>(clients);
    }
}
