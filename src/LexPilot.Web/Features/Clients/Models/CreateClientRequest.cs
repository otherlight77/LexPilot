namespace LexPilot.Web.Features.Clients.Models;

public sealed class CreateClientRequest
{
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telephone { get; set; }
}
