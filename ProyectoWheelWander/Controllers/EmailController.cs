using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Datos;
using ProyectoWheelWander.Models;
using ProyectoWheelWander.Services;

namespace ProyectoWheelWander.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailServices _emailServices;
        UsuarioDatos UsuarioDatos = new UsuarioDatos();
        public EmailController(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }

        [HttpPost]
        public IActionResult enviarEmail(string email)
        {
            string nombre = UsuarioDatos.BuscarUsuarioEmail(email);
            string contrasena = UsuarioDatos.GenerarContrasena();
            bool respuesta = UsuarioDatos.CambiarContrasena(email, contrasena);

            if (nombre!="")
            {
                if (respuesta)
                {
                    var request = new EmailDTO
                    {
                        Para = email,
                        Asunto = "Recuperación de contraseña - Wheel Wander",
                        Contenido = "<div style=\"background-color: WHITE; color: BLACK; font-family: Arial, sans-serif; padding: 20px;\">\r\n" +
                                "<img src=\"https://i.ibb.co/sbqP05H/logo2.png\" alt=\"Logo\" width=\"600px\" height=\"auto\">\r\n" +
                                "<h1 style=\"color: #F3B61F;\">Hola " + nombre + ",</h1>\r\n" +
                                "<p>Has solicitado recuperar tu contraseña. Tu nueva contraseña para iniciar sesión es:</p>\r\n" +
                                "</br>\r\n" +
                                "<p> Contraseña: <strong style=\"color: #F3B61F;\">" + contrasena + "</strong></p>\r\n" +
                                "</br>\r\n" +
                                "<p>Por tu seguridad, te recomendamos que cambies tu contraseña después de iniciar sesión.</p>\r\n" +
                                "<p>Saludos,</p>\r\n" +
                                "<p>El equipo de Wheel Wander</p>\r\n" +
                                "</div>"
                    };
                    try
                    {
                        _emailServices.enviarEmail(request);
                        return RedirectToAction("RecuperarTrue", "Usuarios");
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("RecuperarFalse", "Usuarios");
                    }
                }
            }
            else
            {
                return RedirectToAction("RecuperarFalse", "Usuarios");
            }
            return RedirectToAction("RecuperarFalse", "Usuarios");
        }

    }
}
