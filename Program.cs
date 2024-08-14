using GestionTransacciones.App.interfaces;
using GestionTransacciones.App.Services;
using GestionTransacciones.Data;
using GestionTransacciones.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// AddScoped
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IExcelRepository, ExcelRepository>();

/* DATABASE */
builder.Services.AddDbContext<PruebaContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")
    )
);


/* Configuracion de cookies */
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>{
        options.LoginPath = "/Cliente/Login";
        options.LogoutPath = "/Cliente/LogOut";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Evitar caché del navegador
app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "-1";
    await next();
});

// Autenticacion
app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cliente}/{action=Login}/{id?}");

app.Run();
