using LexPilot.AI.DocumentEngine.Interfaces;
using Tesseract;

namespace LexPilot.AI.DocumentEngine.OCR;

public sealed class TesseractOcrEngine : IOcrEngine
{
    public Task<string> ReadAsync(
        string imageFile,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!File.Exists(imageFile))
            throw new FileNotFoundException(imageFile);

        var tessDataPath = Path.Combine(AppContext.BaseDirectory, "tessdata");

        if (!Directory.Exists(tessDataPath))
        {
            return Task.FromResult(
                "[OCR NON CONFIGURE]\n" +
                "Dossier tessdata introuvable.\n" +
                "Chemin attendu : " + tessDataPath);
        }

        using var engine = new TesseractEngine(tessDataPath, "fra+eng", EngineMode.Default);
        using var image = Pix.LoadFromFile(imageFile);
        using var page = engine.Process(image);

        var text = page.GetText();

        return Task.FromResult(text ?? string.Empty);
    }
}
