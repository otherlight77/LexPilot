using LexPilot.Web.Features.Documents.Models;
using System.Net.Http.Json;

namespace LexPilot.Web.Features.Documents.Services;

public sealed class DocumentApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DocumentApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<DocumentItemDto>> GetDocumentsAsync()
    {
        var client = _httpClientFactory.CreateClient("LexPilotApi");
        return await client.GetFromJsonAsync<List<DocumentItemDto>>("api/documents") ?? [];
    }

    public async Task<bool> UploadAsync(Stream fileStream, string fileName)
    {
        var client = _httpClientFactory.CreateClient("LexPilotApi");

        using var content = new MultipartFormDataContent();
        using var streamContent = new StreamContent(fileStream);

        content.Add(streamContent, "file", fileName);

        var response = await client.PostAsync("api/documents/upload", content);
        return response.IsSuccessStatusCode;
    }
}
