using System.Net.NetworkInformation;
using GestionTerrains.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services nécessaires
builder.Services.AddRazorPages();

// Configurer la base de données
builder.Services.AddDbContext<ReservationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter les sessions
builder.Services.AddDistributedMemoryCache(); // Permet le stockage en mémoire
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

// Activer les sessions avant tout accès à HttpContext.Session
app.UseSession();

// Middleware personnalisé pour vérifier si l'utilisateur est connecté
app.Use(async (context, next) =>
{
    var isAuthenticated = context.Session.GetString("UserRole") != null;

    // Permettre uniquement l'accès à Index ou Login si l'utilisateur n'est pas connecté
    if (!isAuthenticated && !context.Request.Path.StartsWithSegments("/Index") && !context.Request.Path.StartsWithSegments("/Login"))
    {
        context.Response.Redirect("/Index");
        return;
    }

    await next.Invoke();
});



app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
