using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ProyectoWheelWander.Controllers
{
    public class LoginController : Controller
    {

        private readonly WheelWanderContext _context3;

        public LoginController(WheelWanderContext context4)
        {
            _context3 = context4;
        }

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context3.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Contrasena == model.Password);

                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, usuario.Email),
                        new Claim(ClaimTypes.Name, $"{usuario.PrimerNombre} {usuario.PrimerApellido}"),
                        new Claim(ClaimTypes.NameIdentifier, usuario.Cedula.ToString())
                        // Puedes agregar más claims según el rol u otros atributos
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        // Configura las propiedades necesarias
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Details", "Usuarios", new { id = usuario.Cedula });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuario o contraseña inválidos.");
                }
            }
            return View(model);
        }
    }
}
