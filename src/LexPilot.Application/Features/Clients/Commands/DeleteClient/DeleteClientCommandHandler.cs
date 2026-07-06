using LexPilot.Application.Common.Exceptions;
using LexPilot.Application.Common.Interfaces;
using MediatR;

namespace LexPilot.Application.Features.Clients.Commands.DeleteClient;

public sealed class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = _context.Clients.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);

        if (client is null)
            throw new NotFoundException("Client", request.Id);

        client.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
