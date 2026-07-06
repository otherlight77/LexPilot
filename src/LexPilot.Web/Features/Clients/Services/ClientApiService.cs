using LexPilot.Web.Features.Clients.Models;
using LexPilot.Web.Services.Api;

namespace LexPilot.Web.Features.Clients.Services;

public sealed class ClientApiService
{
    private readonly ApiClient _apiClient;

    public ClientApiService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<ClientListItemDto>> GetClientsAsync()
    {
        return await _apiClient.GetAsync<List<ClientListItemDto>>("api/Clients") ?? [];
    }

    public async Task<Guid?> CreateClientAsync(CreateClientRequest request)
    {
        return await _apiClient.PostAsync<CreateClientRequest, Guid>("api/Clients", request);
    }

    public async Task<bool> UpdateClientAsync(UpdateClientRequest request)
    {
        return await _apiClient.PutAsync($"api/Clients/{request.Id}", request);
    }

    public async Task<bool> DeleteClientAsync(Guid id)
    {
        return await _apiClient.DeleteAsync($"api/Clients/{id}");
    }
}
