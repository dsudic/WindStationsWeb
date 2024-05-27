using Microsoft.EntityFrameworkCore;
using WindStations.Core.Interfaces;
using WindStations.Infrastructure.Data;
using WindStations.Infrastructure.Services;
using WindStations.WebUI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();

builder.Services.AddDbContextFactory<WindStationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WindStationsDatabase")),
    ServiceLifetime.Transient);
builder.Services.AddSingleton<IMqttClientService, MqttClientService>();
builder.Services.AddTransient<IMessagePersistenceService, MessagePersistenceService>();

var app = builder.Build();

var mqttClient = app.Services.GetRequiredService<IMqttClientService>();
await mqttClient.ConnectAndSubscribe();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
