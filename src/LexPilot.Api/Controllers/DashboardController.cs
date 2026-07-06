using Microsoft.AspNetCore.Mvc;

namespace LexPilot.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
public sealed class DashboardController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Clients = 0,
            Dossiers = 0,
            Documents = 0,
            Mails = 0,
            IaActive = false,
            Alertes = new[]
            {
                "Aucun module IA configure pour le moment",
                "Microsoft 365 non connecte",
                "Base LexPilot operationnelle"
            },
            ActionsRapides = new[]
            {
                "Creer un client",
                "Creer un dossier",
                "Analyser un document",
                "Connecter Microsoft 365",
                "Generer un courrier"
            }
        });
    }
}
