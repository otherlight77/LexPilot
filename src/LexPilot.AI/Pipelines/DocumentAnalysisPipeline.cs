using LexPilot.AI.Abstractions;
using LexPilot.AI.Models;

namespace LexPilot.AI.Pipelines;

public sealed class DocumentAnalysisPipeline : IDocumentAnalysisPipeline
{
    public Task<DocumentAnalysisResult> AnalyzeAsync(string filePath, CancellationToken cancellationToken)
    {
        var fileName = Path.GetFileName(filePath);
        var lower = fileName.ToLowerInvariant();

        var type = DetectType(lower);
        var client = DetectClient(fileName);
        var dossier = DetectDossier(lower, type);

        var result = new DocumentAnalysisResult
        {
            FileName = fileName,
            DocumentType = type,
            SuggestedClient = client,
            SuggestedDossier = dossier,
            Category = DetectCategory(type),
            Confidence = 0.82m,
            Summary = $"Analyse IA provisoire : le document '{fileName}' semble etre un document de type '{type}'. LexPilot propose un classement automatique et une validation humaine.",
            DetectedDates = new List<string>
            {
                DateTime.Now.ToString("dd/MM/yyyy")
            },
            DetectedAmounts = lower.Contains("facture") || lower.Contains("honoraire")
                ? new List<string> { "Montant potentiel a verifier" }
                : new List<string>(),
            SuggestedActions = new List<ActionSuggestion>
            {
                new() { Label = "Ranger dans le dossier propose", Type = "classification", IsRecommended = true },
                new() { Label = "Generer un resume", Type = "summary", IsRecommended = true },
                new() { Label = "Verifier les delais", Type = "deadline", IsRecommended = type is "Jugement" or "Assignation" },
                new() { Label = "Preparer un courrier", Type = "draft", IsRecommended = true }
            }
        };

        return Task.FromResult(result);
    }

    private static string DetectType(string lowerFileName)
    {
        if (lowerFileName.Contains("jugement")) return "Jugement";
        if (lowerFileName.Contains("assignation")) return "Assignation";
        if (lowerFileName.Contains("contrat")) return "Contrat";
        if (lowerFileName.Contains("facture")) return "Facture";
        if (lowerFileName.Contains("courrier")) return "Courrier";
        if (lowerFileName.Contains("piece")) return "Piece";
        return "Document";
    }

    private static string DetectClient(string fileName)
    {
        var name = Path.GetFileNameWithoutExtension(fileName);

        var separators = new[] { '_', '-', ' ' };
        var parts = name.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length >= 2)
            return $"{parts[0]} {parts[1]}";

        return "Client a confirmer";
    }

    private static string DetectDossier(string lowerFileName, string type)
    {
        if (lowerFileName.Contains("divorce")) return "Divorce";
        if (lowerFileName.Contains("prudhomme") || lowerFileName.Contains("travail")) return "Droit du travail";
        if (lowerFileName.Contains("succession")) return "Succession";
        if (lowerFileName.Contains("immobilier")) return "Immobilier";

        return type switch
        {
            "Assignation" => "Procedure",
            "Jugement" => "Procedure",
            "Contrat" => "Contrats",
            "Facture" => "Facturation",
            _ => "Dossier a confirmer"
        };
    }

    private static string DetectCategory(string type)
    {
        return type switch
        {
            "Assignation" => "Procedure",
            "Jugement" => "Procedure",
            "Contrat" => "Contractuel",
            "Facture" => "Facturation",
            "Courrier" => "Correspondance",
            _ => "General"
        };
    }
}
