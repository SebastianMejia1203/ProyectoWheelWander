using Microsoft.AspNetCore.Authentication.Cookies;
using ProyectoWheelWander.Models;
using System.Configuration;
using QuestPDF.Infrastructure;
using ProyectoWheelWander.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Configura los servicios de MVC o controladores con vistas
builder.Services.AddControllersWithViews();

// A�adir soporte para autorizaci�n
builder.Services.AddAuthorization();

//conexion a la base de datos


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // Define el esquema de autenticaci�n
        .AddCookie("Cookies", options => // Agrega y configura el soporte de cookies
        {
            options.LoginPath = "/Login/Index"; // Ruta al formulario de login
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.AccessDeniedPath = "/Home/Index"; // Ruta para acceso denegado
        });

builder.Services.AddControllersWithViews();

// Configurar la licencia
QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddScoped<IEmailServices, EmailServices>();

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

// Insertar el middleware de autenticaci�n y autorizaci�n en el lugar correcto
app.UseAuthentication(); // Importante si est�s usando autenticaci�n
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

