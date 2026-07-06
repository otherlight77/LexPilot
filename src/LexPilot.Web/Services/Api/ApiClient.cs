using System.Net.Http.Json;

namespace LexPilot.Web.Services.Api;

public sealed class ApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("LexPilotApi");

    public async Task<T?> GetAsync<T>(string url)
    {
        return await Client.GetFromJsonAsync<T>(url);
    }

    public async Task<TResult?> PostAsync<TRequest, TResult>(string url, TRequest request)
    {
        var response = await Client.PostAsJsonAsync(url, request);

        if (!response.IsSuccessStatusCode)
            return default;

        return await response.Content.ReadFromJsonAsync<TResult>();
    }

    public async Task<bool> PutAsync<TRequest>(string url, TRequest request)
    {
        var response = await Client.PutAsJsonAsync(url, request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string url)
    {
        var response = await Client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }
}
