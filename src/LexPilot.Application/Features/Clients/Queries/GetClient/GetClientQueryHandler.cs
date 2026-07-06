using LexPilot.Application.Common.Interfaces;
using MediatR;

namespace LexPilot.Application.Features.Clients.Queries.GetClient;

public sealed class GetClientQueryHandler : IRequestHandler<GetClientQuery, ClientDto?>
{
    private readonly IApplicationDbContext _context;

    public GetClientQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<ClientDto?> Handle(GetClientQuery request, CancellationToken cancellationToken)
    {
        var client = _context.Clients
            .Where(x => x.Id == request.Id && !x.IsDeleted)
            .Select(x => new ClientDto(
                x.Id,
                x.Nom,
                x.Prenom,
                x.Email,
                x.Telephone))
            .FirstOrDefault();

        return Task.FromResult(client);
    }
}
