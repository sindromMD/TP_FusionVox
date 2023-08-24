using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;
using TP_FusionVox.Services;

var builder = WebApplication.CreateBuilder(args); // Crée une web app avec les paramètres envoyés

// Injecter la localisation
#region Localizer configuration
CultureInfo[] supportedCultures = new[]
{
    new CultureInfo("fr-CA"),
    new CultureInfo("en-US"),
    new CultureInfo("ro-RO")
};
#endregion

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Ajouter des services au conteneur.
builder.Services.AddControllersWithViews()// Permet MVC
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddRazorPages(); // Permet utilisation de Razor
builder.Services.AddMvc().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<BaseDeDonnees>(); // Permet l'utilisation du Singleton

builder.Services.AddDbContext<TP_FusionVoxDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseLazyLoadingProxies();
});

builder.Services.AddDistributedMemoryCache(); // Permet l'utilisation de cookies
builder.Services.AddSession(option => { option.IdleTimeout = TimeSpan.FromMinutes(20); }); // Configure l'expiration d'un cookies,
builder.Services.AddScoped(typeof(IServiceBaseAsync<>), typeof(ServiceBaseAsync<>));
builder.Services.AddScoped<IGenreMusicalService, GenreMusicalService>();
builder.Services.AddScoped<IArtisteService, ArtisteService>();
builder.Services.AddScoped<IConcertService, ConcertService>();
builder.Services.AddScoped<IAgentService, AgentService>();

var app = builder.Build();

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = context => context.Context.Response.Headers.Add("Cache-Control", "no-cache")
    });
}
else
{
    app.UseStaticFiles();
}

app.UseSession(); // Permet l'utilisation de cookies

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}",
        defaults: new { controller = "Home", action = "Index" });
});

app.MapRazorPages();
app.Run();

// Doc
// Environnements: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-7.0
// Fichiers statiques et wwwroot : https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-7.0
// Gestion de la cache : https://learn.microsoft.com/en-us/aspnet/core/performance/caching/response?view=aspnetcore-7.0