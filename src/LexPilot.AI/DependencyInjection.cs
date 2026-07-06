using LexPilot.AI.Abstractions;
using LexPilot.AI.Agents;
using LexPilot.AI.Pipelines;
using LexPilot.AI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LexPilot.AI;

public static class DependencyInjection
{
    public static IServiceCollection AddLexPilotAI(this IServiceCollection services)
    {
        services.AddScoped<IDocumentAnalysisPipeline, DocumentAnalysisPipeline>();

        services.AddScoped<CopilotAgent>();
        services.AddScoped<ICopilotService, CopilotService>();

        return services;
    }
}
