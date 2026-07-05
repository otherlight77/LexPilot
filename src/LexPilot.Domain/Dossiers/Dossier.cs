using LexPilot.Domain.Common;
using LexPilot.Domain.Clients;

namespace LexPilot.Domain.Dossiers;

public class Dossier : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public string Titre { get; set; } = string.Empty;
    public string Nature { get; set; } = string.Empty;
    public string Etat { get; set; } = "Ouvert";
    public string? Juridiction { get; set; }
    public DateTime DateOuvertureUtc { get; set; } = DateTime.UtcNow;
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
}
