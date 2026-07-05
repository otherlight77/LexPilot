namespace LexPilot.Application.Clients;

public record ClientDto(Guid Id, string Civilite, string Nom, string Prenom, string? Societe, string? Email, string? Telephone, string? Adresse);
public record CreateClientRequest(string Civilite, string Nom, string Prenom, string? Societe, string? Email, string? Telephone, string? Adresse);
