using System.Net.NetworkInformation;
using GestionTerrains.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services n�cessaires
builder.Services.AddRazorPages();

// Configurer la base de donn�es
builder.Services.AddDbContext<ReservationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter les sessions
builder.Services.AddDistributedMemoryCache(); // Permet le stockage en m�moire
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

// Activer les sessions avant tout acc�s � HttpContext.Session
app.UseSession();

// Middleware personnalis� pour v�rifier si l'utilisateur est connect�
app.Use(async (context, next) =>
{
    var isAuthenticated = context.Session.GetString("UserRole") != null;

    // Permettre uniquement l'acc�s � Index ou Login si l'utilisateur n'est pas connect�
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
