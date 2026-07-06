using LexPilot.Web.Features.Copilot.Models;
using LexPilot.Web.Services.Api;

namespace LexPilot.Web.Features.Copilot.Services;

public sealed class CopilotApiService
{
    private readonly ApiClient _apiClient;

    public CopilotApiService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<CopilotChatResponse?> SendAsync(string message)
    {
        var request = new CopilotChatRequest
        {
            Message = message
        };

        return await _apiClient.PostAsync<CopilotChatRequest, CopilotChatResponse>(
            "api/copilot/chat",
            request);
    }
}
