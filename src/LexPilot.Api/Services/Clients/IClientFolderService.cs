using LexPilot.Domain.Clients;

namespace LexPilot.Api.Services.Clients;

public interface IClientFolderService
{
    Task<Client> CreateClientAsync(
        string firstName,
        string lastName,
        string email,
        string phone);

    Task CreateClientFolderTreeAsync(Client client);

    string GetClientFolder(Client client);
}
