using System.Text.RegularExpressions;
using LexPilot.AI.DocumentEngine.Interfaces;

namespace LexPilot.AI.DocumentEngine.Services;

public sealed class TextCleaner : ITextCleaner
{
    public string Clean(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        text = text.Replace("\r", " ");
        text = text.Replace("\n", " ");
        text = Regex.Replace(text, @"\s+", " ");

        return text.Trim();
    }
}
