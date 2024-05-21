using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Datos;
using ProyectoWheelWander.Models;
using ProyectoWheelWander.Models.Data;
using ProyectoWheelWander.Models.ViewModel;
using System.Numerics;
using System.Security.Claims;

namespace ProyectoWheelWander.Controllers
{

    public class MotoController : Controller
    {
        // GET: Obtener Pagina de Administrar mis motos
        MotoDatos _MotoDatos = new MotoDatos();
        CatalogoDatos catalogoService = new CatalogoDatos();

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
            var viewModel = new CrearMotoViewModel {
                moto = new Moto(),
                ubicaciones = _MotoDatos.GetAllUbicaciones(),
                marcas = catalogoService.GetAllMarcas(),
                transmiciones = _MotoDatos.GetTransmicion()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult crearMoto(CrearMotoViewModel model)
        {
            var cedulaAutenticada = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            int cedulsa = Convert.ToInt32(cedulaAutenticada);
            model.moto.FkcedulaPropietario = cedulsa;
            var respuesta = _MotoDatos.InsertarMoto(model.moto);

           
                return RedirectToAction("Index", "Home");
            
        }

        [HttpGet]
        public IActionResult Detalle(string placa)
        {
            var cedulaAutenticada = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            int cedula = Convert.ToInt32(cedulaAutenticada);
            var ganancias = _MotoDatos.FindUsuarioByCedula(cedula);
            var viewModel = new AdminMotoViewModel
            {
                listaMotos = _MotoDatos.motosPorUsuario(cedula),
                reserva = _MotoDatos.ObtenerUltimaReservaValidaPorPlaca(placa),
                historial = _MotoDatos.ObtenerHistorial(placa),
                gananciaActual = ganancias.gananciaA,
                gananciaHistorica = ganancias.gananciaH
            };
            viewModel.reserva.FechaHoraInicio = viewModel.reserva.FechaInicio + viewModel.reserva.HoraInicio;
            viewModel.reserva.FechaHoraFin = viewModel.reserva.FechaFin + viewModel.reserva.HoraFin;
            viewModel.tiempo = (viewModel.reserva.FechaHoraFin) - (viewModel.reserva.FechaHoraInicio);
            double horas = viewModel.tiempo.TotalHours;
            viewModel.duracion = viewModel.tiempo.Days + " Dia(s), " + viewModel.tiempo.Hours + "Hora(s)";

            return View(viewModel);
        }

        public IActionResult inhabilitar(string placa) {
            var placs = new inhabilitarViewModel
            {
                placa = placa
            };
            return View(placs);
        }

        [HttpPost]
        public IActionResult inhabilitar(inhabilitarViewModel model)
        {
            var respuesta = _MotoDatos.inhabilitarMoto(model.placa);

            if (respuesta)
            {
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
