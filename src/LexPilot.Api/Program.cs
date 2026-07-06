using LexPilot.AI;
using LexPilot.Application.Common.Interfaces;
using LexPilot.Application;
using LexPilot.Api.Services.Clients;
using LexPilot.Infrastructure.Mail;
using LexPilot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddLexPilotAI(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LexPilotDbContext>(options =>
    options.UseInMemoryDatabase("LexPilotDev"));

builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<LexPilotDbContext>());

builder.Services.Configure<OvhMailSettings>(builder.Configuration.GetSection("OvhMail"));
builder.Services.AddScoped<OvhMailService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("LexPilotCors", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddScoped<IClientFolderService, ClientFolderService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("LexPilotCors");
app.MapControllers();

app.MapGet("/", () => new
{
    app = "LexPilot Enterprise",
    version = "0.1",
    status = "running"
});

app.Run();






