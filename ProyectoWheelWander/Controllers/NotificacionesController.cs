using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoWheelWander.Controllers
{
    public class NotificacionesController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // GET: Notificaciones
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Notificacion()
        {
            return View();
        }

        public IActionResult NotificacionError()
        {
            return View();
        }
        public IActionResult RecuperarCuenta()
        {
            return View();
        }
    }
}
