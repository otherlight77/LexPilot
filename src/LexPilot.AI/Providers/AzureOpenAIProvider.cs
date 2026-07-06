using LexPilot.AI.Chat;
using LexPilot.AI.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace LexPilot.AI.Providers;

public sealed class AzureOpenAIProvider : BaseAIProvider
{
    private readonly HttpClient _httpClient;
    private readonly AIProviderOptions _options;

    public AzureOpenAIProvider(HttpClient httpClient, IOptions<AIProviderOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public override string Name => "AzureOpenAI";

    public override async Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_options.AzureOpenAIEndpoint) ||
            string.IsNullOrWhiteSpace(_options.AzureOpenAIApiKey) ||
            string.IsNullOrWhiteSpace(_options.AzureOpenAIDeployment))
        {
            return new ChatResponse
            {
                ConversationId = request.ConversationId,
                Answer = "Configuration Azure OpenAI incomplete.",
                SuggestedActions = ["Configurer endpoint", "Configurer deployment", "Configurer cle API"]
            };
        }

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("api-key", _options.AzureOpenAIApiKey);

        var endpoint = _options.AzureOpenAIEndpoint.TrimEnd('/');
        var url = $"{endpoint}/openai/deployments/{_options.AzureOpenAIDeployment}/chat/completions?api-version=2024-02-15-preview";

        var payload = new AzureOpenAIRequest
        {
            Temperature = _options.Temperature,
            MaxTokens = _options.MaxTokens,
            Messages =
            [
                new AzureOpenAIMessage { Role = "system", Content = "Tu es LexPilot, assistant IA pour cabinet d'avocats." },
                .. request.History.Select(x => new AzureOpenAIMessage
                {
                    Role = x.Role.ToString().ToLowerInvariant() == "assistant" ? "assistant" : "user",
                    Content = x.Content
                }),
                new AzureOpenAIMessage { Role = "user", Content = request.Message }
            ]
        };

        var response = await _httpClient.PostAsJsonAsync(url, payload, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return new ChatResponse
            {
                ConversationId = request.ConversationId,
                Answer = $"Erreur Azure OpenAI : {(int)response.StatusCode} {response.ReasonPhrase}",
                SuggestedActions = ["Verifier endpoint", "Verifier deployment", "Repasser en Fake"]
            };
        }

        var result = await response.Content.ReadFromJsonAsync<AzureOpenAIResponse>(cancellationToken: cancellationToken);

        return new ChatResponse
        {
            ConversationId = request.ConversationId,
            Answer = result?.Choices?.FirstOrDefault()?.Message?.Content ?? "Azure OpenAI n'a retourne aucune reponse.",
            SuggestedActions = ["Resumer", "Generer un courrier", "Verifier les echeances"]
        };
    }

    private sealed class AzureOpenAIRequest
    {
        [JsonPropertyName("messages")]
        public List<AzureOpenAIMessage> Messages { get; set; } = [];

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; }
    }

    private sealed class AzureOpenAIMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
    }

    private sealed class AzureOpenAIResponse
    {
        [JsonPropertyName("choices")]
        public List<AzureOpenAIChoice> Choices { get; set; } = [];
    }

    private sealed class AzureOpenAIChoice
    {
        [JsonPropertyName("message")]
        public AzureOpenAIMessage? Message { get; set; }
    }
}
