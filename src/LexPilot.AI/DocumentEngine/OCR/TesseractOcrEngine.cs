using LexPilot.AI.DocumentEngine.Interfaces;

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

        var fileName = Path.GetFileName(imageFile);

        var text =
            "[OCR PLACEHOLDER]" +
            Environment.NewLine +
            fileName +
            Environment.NewLine +
            "Le moteur OCR est pret. Le branchement Tesseract reel sera ajoute dans le prochain lot.";

        return Task.FromResult(text);
    }
}
