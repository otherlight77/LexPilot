using LexPilot.Domain.Common;
using LexPilot.Domain.Dossiers;

namespace LexPilot.Domain.Clients;

public class Client : BaseEntity
{
    public string Civilite { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string? Societe { get; set; }
    public string? Email { get; set; }
    public string? Telephone { get; set; }
    public string? Adresse { get; set; }
    public ICollection<Dossier> Dossiers { get; set; } = new List<Dossier>();
}
