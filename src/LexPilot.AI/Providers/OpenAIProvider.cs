using LexPilot.AI.Chat;
using LexPilot.AI.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace LexPilot.AI.Providers;

public sealed class OpenAIProvider : BaseAIProvider
{
    private readonly HttpClient _httpClient;
    private readonly AIProviderOptions _options;

    public OpenAIProvider(HttpClient httpClient, IOptions<AIProviderOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public override string Name => "OpenAI";

    public override async Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_options.OpenAIApiKey))
        {
            return MissingConfiguration(request, "OpenAI API key manquante.");
        }

        _httpClient.BaseAddress = new Uri("https://api.openai.com/");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.OpenAIApiKey);

        var payload = new OpenAIRequest
        {
            Model = _options.OpenAIModel,
            Temperature = _options.Temperature,
            MaxTokens = _options.MaxTokens,
            Messages =
            [
                new OpenAIMessage { Role = "system", Content = "Tu es LexPilot, assistant IA pour cabinet d'avocats. Reponds clairement, prudemment, et propose des actions utiles." },
                .. request.History.Select(x => new OpenAIMessage
                {
                    Role = x.Role.ToString().ToLowerInvariant() == "assistant" ? "assistant" : "user",
                    Content = x.Content
                }),
                new OpenAIMessage { Role = "user", Content = request.Message }
            ]
        };

        var response = await _httpClient.PostAsJsonAsync("v1/chat/completions", payload, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return new ChatResponse
            {
                ConversationId = request.ConversationId,
                Answer = $"Erreur OpenAI : {(int)response.StatusCode} {response.ReasonPhrase}",
                SuggestedActions = ["Verifier la cle API", "Verifier le modele", "Repasser en provider Fake"]
            };
        }

        var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>(cancellationToken: cancellationToken);

        return new ChatResponse
        {
            ConversationId = request.ConversationId,
            Answer = result?.Choices?.FirstOrDefault()?.Message?.Content ?? "OpenAI n'a retourne aucune reponse.",
            SuggestedActions = ["Resumer", "Generer un courrier", "Verifier les echeances"]
        };
    }

    private static ChatResponse MissingConfiguration(ChatRequest request, string message)
    {
        return new ChatResponse
        {
            ConversationId = request.ConversationId,
            Answer = message,
            SuggestedActions = ["Configurer OpenAI", "Utiliser Fake", "Utiliser Ollama"]
        };
    }

    private sealed class OpenAIRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;

        [JsonPropertyName("messages")]
        public List<OpenAIMessage> Messages { get; set; } = [];

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; }
    }

    private sealed class OpenAIMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
    }

    private sealed class OpenAIResponse
    {
        [JsonPropertyName("choices")]
        public List<OpenAIChoice> Choices { get; set; } = [];
    }

    private sealed class OpenAIChoice
    {
        [JsonPropertyName("message")]
        public OpenAIMessage? Message { get; set; }
    }
}
