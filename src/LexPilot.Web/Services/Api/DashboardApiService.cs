namespace LexPilot.Web.Services.Api;

public sealed class DashboardApiService
{
    private readonly ApiClient _apiClient;

    public DashboardApiService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<DashboardDto?> GetDashboardAsync()
    {
        return await _apiClient.GetAsync<DashboardDto>("api/dashboard");
    }
}

public sealed class DashboardDto
{
    public int Clients { get; set; }
    public int Dossiers { get; set; }
    public int Documents { get; set; }
    public int Mails { get; set; }
    public bool IaActive { get; set; }
    public string[] Alertes { get; set; } = [];
    public string[] ActionsRapides { get; set; } = [];
}
