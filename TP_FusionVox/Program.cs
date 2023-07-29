using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Models.Data;
using TP2.Models;

var builder = WebApplication.CreateBuilder(args); // Crée une web app avec les paramètres envoyés
builder.Services.AddControllersWithViews(); // Permet MVC
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

var app = builder.Build();

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