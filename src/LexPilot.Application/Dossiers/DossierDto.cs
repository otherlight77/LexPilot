namespace LexPilot.Application.Dossiers;

public record DossierDto(Guid Id, string Numero, string Titre, string Nature, string Etat, string? Juridiction, Guid ClientId);
public record CreateDossierRequest(string Numero, string Titre, string Nature, string? Juridiction, Guid ClientId);
