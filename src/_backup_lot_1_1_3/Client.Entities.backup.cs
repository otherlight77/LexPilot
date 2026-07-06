using LexPilot.Domain.Common;

namespace LexPilot.Domain.Entities;

public sealed class Client : BaseEntity
{
    public string Nom { get; set; } = string.Empty;

    public string Prenom { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public string? Adresse { get; set; }

    public string? CodePostal { get; set; }

    public string? Ville { get; set; }
}
