using LexPilot.Domain.Dossiers;

namespace LexPilot.Domain.Clients;

public class Client
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nom { get; set; } = string.Empty;

    public string Prenom { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Telephone { get; set; } = string.Empty;

    public DateTime DateCreation { get; set; } = DateTime.UtcNow;

    public bool IsDeleted { get; set; } = false;

    public List<Dossier> Dossiers { get; set; } = new();
}



