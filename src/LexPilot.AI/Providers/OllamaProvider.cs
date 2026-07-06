using LexPilot.AI.Chat;
using LexPilot.AI.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace LexPilot.AI.Providers;

public sealed class OllamaProvider : BaseAIProvider
{
    private readonly HttpClient _httpClient;
    private readonly AIProviderOptions _options;

    public OllamaProvider(HttpClient httpClient, IOptions<AIProviderOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public override string Name => "Ollama";

    public override async Task<ChatResponse> SendAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var endpoint = _options.OllamaEndpoint.TrimEnd('/');
        var url = $"{endpoint}/api/chat";

        var payload = new OllamaRequest
        {
            Model = _options.OllamaModel,
            Stream = false,
            Messages =
            [
                new OllamaMessage { Role = "system", Content = "Tu es LexPilot, assistant IA pour cabinet d'avocats." },
                .. request.History.Select(x => new OllamaMessage
                {
                    Role = x.Role.ToString().ToLowerInvariant() == "assistant" ? "assistant" : "user",
                    Content = x.Content
                }),
                new OllamaMessage { Role = "user", Content = request.Message }
            ]
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, payload, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return new ChatResponse
                {
                    ConversationId = request.ConversationId,
                    Answer = $"Erreur Ollama : {(int)response.StatusCode} {response.ReasonPhrase}",
                    SuggestedActions = ["Verifier Ollama", "Verifier le modele", "Repasser en Fake"]
                };
            }

            var result = await response.Content.ReadFromJsonAsync<OllamaResponse>(cancellationToken: cancellationToken);

            return new ChatResponse
            {
                ConversationId = request.ConversationId,
                Answer = result?.Message?.Content ?? "Ollama n'a retourne aucune reponse.",
                SuggestedActions = ["Resumer", "Generer un courrier", "Verifier les echeances"]
            };
        }
        catch (Exception ex)
        {
            return new ChatResponse
            {
                ConversationId = request.ConversationId,
                Answer = "Ollama indisponible : " + ex.Message,
                SuggestedActions = ["Demarrer Ollama", "Installer le modele", "Utiliser Fake"]
            };
        }
    }

    private sealed class OllamaRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;

        [JsonPropertyName("stream")]
        public bool Stream { get; set; }

        [JsonPropertyName("messages")]
        public List<OllamaMessage> Messages { get; set; } = [];
    }

    private sealed class OllamaMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
    }

    private sealed class OllamaResponse
    {
        [JsonPropertyName("message")]
        public OllamaMessage? Message { get; set; }
    }
}
