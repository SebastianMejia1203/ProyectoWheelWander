using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Datos;
using ProyectoWheelWander.Models;
using ProyectoWheelWander.Models.Data;
using ProyectoWheelWander.Models.ViewModel;
using System.Globalization;

namespace ProyectoWheelWander.Controllers
{
    public class CatalogoController : Controller
    {
        CatalogoDatos catalogoService = new CatalogoDatos();
        // GET: CatalogoController
        public ActionResult Index()
        {
            var viewModel = new CatalogoViewModel
            {
                motosCatalogo = catalogoService.getCatalogo(),
                ubicaciones = catalogoService.GetAllUbicaciones(),
                marcas = catalogoService.GetAllMarcas()
            };
            return View(viewModel);
        }

        public ActionResult FiltrarMotos(int? ubicacionId, int? marcaId, int? claseId, int? ordenarPorNombre, int? ordenarPorPrecio)
        {
            var viewModel = new CatalogoViewModel
            {
                motosCatalogo = catalogoService.FiltrarMotos(ubicacionId, marcaId, claseId, ordenarPorNombre, ordenarPorPrecio),
                ubicaciones = catalogoService.GetAllUbicaciones(),
                marcas = catalogoService.GetAllMarcas()
            };

            return View("Index", viewModel);
        }

        public ActionResult FiltrarMotosFecha(int? ubicacionId, DateTime? fechaInicio, TimeSpan? horaInicio, DateTime? fechaFin, TimeSpan? horaFin)
        {
            var viewModel = new CatalogoViewModel
            {
                motosCatalogo = catalogoService.FiltrarMotosFecha(ubicacionId, fechaInicio, horaInicio, fechaFin, horaFin),
                ubicaciones = catalogoService.GetAllUbicaciones(),
                marcas = catalogoService.GetAllMarcas()
            };

            return View("Index", viewModel);
        }

        public ActionResult FiltrarMotosFecha2(int? ubicacionId, int? marcaId, int? claseId, DateTime fechaInicio, TimeSpan horaInicio, DateTime fechaFin, TimeSpan horaFin, int? ordenarPorNombre, int? ordenarPorPrecio)
        {

            var viewModel = new CatalogoViewModel
            {
                motosCatalogo = catalogoService.FiltrarMotos2(ubicacionId, marcaId, claseId, fechaInicio, horaInicio, fechaFin, horaFin, ordenarPorNombre, ordenarPorPrecio),
                ubicaciones = catalogoService.GetAllUbicaciones(),
                marcas = catalogoService.GetAllMarcas(),
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                HoraInicio = horaInicio,
                HoraFin = horaFin,
            };
            
            
            return View("Index", viewModel);
        }

    }
}
