namespace LexPilot.AI.Configuration;

public sealed class AIProviderOptions
{
    public string Provider { get; set; } = "Fake";

    public string? OpenAIApiKey { get; set; }
    public string OpenAIModel { get; set; } = "gpt-4o-mini";

    public string? AzureOpenAIEndpoint { get; set; }
    public string? AzureOpenAIApiKey { get; set; }
    public string? AzureOpenAIDeployment { get; set; }

    public string OllamaEndpoint { get; set; } = "http://localhost:11434";
    public string OllamaModel { get; set; } = "llama3.1";

    public string? AnthropicApiKey { get; set; }
    public string AnthropicModel { get; set; } = "claude-sonnet-4";

    public string? GeminiApiKey { get; set; }
    public string GeminiModel { get; set; } = "gemini-2.5-pro";

    public int TimeoutSeconds { get; set; } = 120;
    public bool EnableStreaming { get; set; } = true;
    public bool EnableLogging { get; set; } = true;
    public double Temperature { get; set; } = 0.2;
    public int MaxTokens { get; set; } = 4096;
}
