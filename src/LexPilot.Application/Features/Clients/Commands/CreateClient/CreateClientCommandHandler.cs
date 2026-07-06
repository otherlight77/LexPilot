using LexPilot.Application.Common.Interfaces;
using LexPilot.Domain.Clients;
using MediatR;

namespace LexPilot.Application.Features.Clients.Commands.CreateClient;

public sealed class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            Nom = request.Nom,
            Prenom = request.Prenom,
            Email = request.Email ?? string.Empty,
            Telephone = request.Telephone ?? string.Empty,
            DateCreation = DateTime.UtcNow,
            IsDeleted = false
        };

        _context.AddClient(client);

        await _context.SaveChangesAsync(cancellationToken);

        return client.Id;
    }
}
