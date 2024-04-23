using Microsoft.EntityFrameworkCore;
using ProyectoWheelWander.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configura los servicios de MVC o controladores con vistas
builder.Services.AddControllersWithViews();

// Añadir soporte para autorización
builder.Services.AddAuthorization();

//conexion a la base de datos
builder.Services.AddDbContext<WheelWanderContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("conexionDb")));

builder.Services.AddAuthentication("Cookies") // Define el esquema de autenticación
        .AddCookie("Cookies", options => // Agrega y configura el soporte de cookies
        {
            options.LoginPath = "/Login/Index"; // Ruta al formulario de login
            options.AccessDeniedPath = "/Home/Index"; // Ruta para acceso denegado
        });

builder.Services.AddControllersWithViews();

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

// Insertar el middleware de autenticación y autorización en el lugar correcto
app.UseAuthentication(); // Importante si estás usando autenticación
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

