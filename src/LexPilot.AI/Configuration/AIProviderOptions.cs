namespace LexPilot.AI.Configuration;

public sealed class AIProviderOptions
{
    public string Provider { get; set; } = "Fake";

    public string? OpenAIApiKey { get; set; }

    public string? OpenAIModel { get; set; } = "gpt-4o-mini";

    public string? AzureOpenAIEndpoint { get; set; }

    public string? AzureOpenAIApiKey { get; set; }

    public string? AzureOpenAIDeployment { get; set; }

    public string? OllamaEndpoint { get; set; } = "http://localhost:11434";

    public string? OllamaModel { get; set; } = "llama3.1";
}
