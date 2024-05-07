using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Datos;
using ProyectoWheelWander.Models.ViewModel;
using System.Security.Claims;

namespace ProyectoWheelWander.Controllers
{

    public class MotoController : Controller
    {
        // GET: Obtener Pagina de Administrar mis motos
        MotoDatos _MotoDatos = new MotoDatos();
        public IActionResult Index()
        {
            var cedulaAutenticada = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            int cedula = Convert.ToInt32(cedulaAutenticada);
            var view = new AdminMotoViewModel
            {
                listaMotos = _MotoDatos.motosPorUsuario(cedula)
            };

            return View(view);
        }

        public IActionResult buscarMoto(String placa)
        {
            //la vista mostrara una lista de usuarios
            var listaMotos = _MotoDatos.buscarMoto(placa);

            return View(listaMotos);
        }

        public IActionResult CrearMoto()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetMotoDetails(string placa)
        {
            var detallesMoto = _MotoDatos.ObtenerUltimaReservaValidaPorPlaca(placa);
            if (detallesMoto == null)
            {
                return NotFound();
            }
            return Json(detallesMoto);
        }

    }
}
