using LexPilot.Domain.Clients;

namespace LexPilot.Api.Services.Clients;

public class ClientFolderService : IClientFolderService
{
    private readonly IWebHostEnvironment _env;

    public ClientFolderService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<Client> CreateClientAsync(
        string firstName,
        string lastName,
        string email,
        string phone)
    {
        var client = new Client
        {
            Prenom = firstName,
            Nom = lastName,
            Email = email,
            Telephone = phone,
            DateCreation = DateTime.UtcNow
        };

        await CreateClientFolderTreeAsync(client);

        return client;
    }

    public string GetClientFolder(Client client)
    {
        var folderName = $"{client.Nom.ToUpper()}_{client.Prenom}_{client.Id}";

        return Path.Combine(
            _env.ContentRootPath,
            "Clients",
            folderName);
    }

    public async Task CreateClientFolderTreeAsync(Client client)
    {
        var root = GetClientFolder(client);

        Directory.CreateDirectory(root);

        string[] folders =
        {
            "01_EtatCivil",
            "02_Courriers",
            "02_Courriers\\Entrants",
            "02_Courriers\\Sortants",
            "02_Courriers\\Barreau",
            "02_Courriers\\Tribunal",
            "02_Courriers\\Huissier",
            "02_Courriers\\Adversaire",
            "03_Procedures",
            "04_Conclusions",
            "05_Pieces",
            "05_Pieces\\Demandeur",
            "05_Pieces\\Defendeur",
            "05_Pieces\\Temoins",
            "05_Pieces\\Expertises",
            "05_Pieces\\Photos",
            "05_Pieces\\Videos",
            "06_Factures",
            "07_Comptabilite",
            "08_Emails",
            "09_Audios",
            "10_IA",
            "10_IA\\Resumes",
            "10_IA\\Analyses",
            "10_IA\\OCR",
            "10_IA\\Audio",
            "10_IA\\Chronologie",
            "10_IA\\RechercheJuridique",
            "11_Documents",
            "12_Archives"
        };

        foreach (var folder in folders)
        {
            Directory.CreateDirectory(Path.Combine(root, folder));
        }

        await Task.CompletedTask;
    }
}

