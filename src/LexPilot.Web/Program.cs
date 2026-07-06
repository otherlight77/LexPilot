using LexPilot.Web.Features.Copilot.Services;
using LexPilot.Web.Features.Documents.Services;
using LexPilot.Web.Services.Api;
using LexPilot.Web.Services.Notifications;
using LexPilot.Web.Services.State;
using LexPilot.Web.Features.Clients.Services;
using LexPilot.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("LexPilotApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5000/");
});

builder.Services.AddScoped<ClientApiService>();

builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<DashboardApiService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<UserState>();
builder.Services.AddScoped<DocumentApiService>();
builder.Services.AddScoped<CopilotApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();





