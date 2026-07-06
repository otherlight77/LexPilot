using LexPilot.AI.Abstractions;
using LexPilot.AI.Agents;
using LexPilot.AI.Chunking;
using LexPilot.AI.Configuration;
using LexPilot.AI.Embeddings;
using LexPilot.AI.Memory;
using LexPilot.AI.Pipelines;
using LexPilot.AI.Providers;
using LexPilot.AI.RAG;
using LexPilot.AI.Services;
using LexPilot.AI.VectorStore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LexPilot.AI;

public static class DependencyInjection
{
    public static IServiceCollection AddLexPilotAI(this IServiceCollection services, IConfiguration? configuration = null)
    {
        services.AddScoped<IDocumentAnalysisPipeline, DocumentAnalysisPipeline>();

        services.AddScoped<CopilotAgent>();

        if (configuration is not null)
        {
            services.Configure<AIProviderOptions>(configuration.GetSection("LexPilotAI"));
        }
        else
        {
            services.Configure<AIProviderOptions>(_ => { });
        }

        services.AddHttpClient<OpenAIProvider>();
        services.AddHttpClient<AzureOpenAIProvider>();
        services.AddHttpClient<OllamaProvider>();

        services.AddScoped<FakeAIProvider>();
        services.AddScoped<AnthropicProvider>();
        services.AddScoped<GeminiProvider>();

        services.AddScoped<IAIProviderFactory, AIProviderFactory>();

        services.AddSingleton<IConversationMemory, InMemoryConversationMemory>();
        services.AddScoped<ContextBuilder>();
        services.AddScoped<PromptBuilder>();

        services.AddSingleton<IVectorStore, InMemoryVectorStore>();
        services.AddScoped<IEmbeddingService, DeterministicEmbeddingService>();
        services.AddScoped<ITextChunker, TextChunker>();
        services.AddScoped<IRagService, RagService>();

        services.AddScoped<ICopilotService, CopilotService>();

        return services;
    }
}
