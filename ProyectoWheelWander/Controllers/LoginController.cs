using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Models.Data;
using ProyectoWheelWander.Datos;
using System.Data;
using System.Data.SqlClient;
using ProyectoWheelWander.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ProyectoWheelWander.Controllers
{
    public class LoginController : Controller
    {

        UsuarioDatos _UsuarioDatos = new UsuarioDatos();

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            Usuario usuario = null;  // Inicializa a null para manejar casos donde no se encuentren datos
            ConexionDB cn = new ConexionDB();
            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ValidarUsuario", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Contrasena", model.Password);

                int cedula = 0;
                try
                {
                    cedula = Convert.ToInt32(cmd.ExecuteScalar());
                    usuario = _UsuarioDatos.FindUsuarioByCedula(cedula);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al validar el usuario: " + ex.Message);
                    throw; // Lanza la excepción para manejo externo
                }
                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, (usuario.Cedula).ToString()),
                        new Claim("PrimerNombre", usuario.PrimerNombre),
                        new Claim("Email", usuario.Email),
                        new Claim(ClaimTypes.Role, (usuario.IDRol).ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }

                return View();
            }
        }

        public async Task<ActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login");
        }


    }
}
