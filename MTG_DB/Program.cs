using Microsoft.EntityFrameworkCore;
using MTG_DB.Components;
using MTG_DB.Data;
using MtgInventoryApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<SettingsService>();
builder.Services.AddMemoryCache();

builder.Services.AddHttpClient<ScryfallService>(client =>
{
    client.BaseAddress = new Uri("https://api.scryfall.com/");
    client.DefaultRequestHeaders.Add("User-Agent", "MtgInventoryApp/1.0 (sapphirepro190@gmail.com)");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<CryptMtgService>(client =>
{
    client.BaseAddress = new Uri("https://cryptmtg.com/");
    client.DefaultRequestHeaders.Add("User-Agent", "MtgInventoryApp/1.0 (sapphirepro190@gmail.com)");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddDbContext<CollectionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CollectionService>();

var app = builder.Build();

// Ensure Supabase database schema is created on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CollectionDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
