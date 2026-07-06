using LexPilot.AI.Abstractions;
using LexPilot.AI.Agents;
using LexPilot.AI.Configuration;
using LexPilot.AI.Pipelines;
using LexPilot.AI.Providers;
using LexPilot.AI.Services;
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

        services.AddScoped<FakeAIProvider>();
        services.AddScoped<OpenAIProvider>();
        services.AddScoped<AzureOpenAIProvider>();
        services.AddScoped<OllamaProvider>();

        services.AddScoped<IAIProviderFactory, AIProviderFactory>();
        services.AddScoped<ICopilotService, CopilotService>();

        return services;
    }
}
