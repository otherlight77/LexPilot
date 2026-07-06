using LexPilot.Application.Common.Exceptions;
using LexPilot.Application.Common.Interfaces;
using MediatR;

namespace LexPilot.Application.Features.Clients.Commands.UpdateClient;

public sealed class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = _context.Clients.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

        if (client is null)
            throw new NotFoundException("Client", request.Id);

        client.Nom = request.Nom;
        client.Prenom = request.Prenom;
        client.Email = request.Email ?? string.Empty;
        client.Telephone = request.Telephone ?? string.Empty;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
