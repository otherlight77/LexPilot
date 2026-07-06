using LexPilot.Domain.Clients;

namespace LexPilot.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    IQueryable<Client> Clients { get; }

    void AddClient(Client client);

    void RemoveClient(Client client);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
