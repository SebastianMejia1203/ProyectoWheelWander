using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Datos;
using ProyectoWheelWander.Models.Data;
using ProyectoWheelWander.Models.ViewModel;
using System.Security.Claims;

namespace ProyectoWheelWander.Controllers
{
    public class ReservaController : Controller
    {
        MotoDatos motoService = new MotoDatos();
        ReservaDatos reservaService = new ReservaDatos();

        // GET: Reserva
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ResumenCotizacion(string placa, DateTime fechaInicio, TimeSpan horaInicio, DateTime fechaFin, TimeSpan horaFin, string ubicacion)
        {
            var cedulaAutenticada = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            int cedula = Convert.ToInt32(cedulaAutenticada);
            var viewModel = new ResumenCotizacionViewModel
            {
                moto = motoService.buscarMoto(placa),
                reserva = new Models.Data.Reserva
                {
                    FKPlacaMoto = placa,
                    FechaHoraInicio = fechaInicio + horaInicio,
                    FechaHoraFin = fechaFin + horaFin,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    HoraInicio = horaInicio,
                    HoraFin = horaFin,
                    FKCedulaUsuario = cedula,
                    
                }
                
            };
            viewModel.reserva.FKIDUbicacion = viewModel.moto.Fkidubicacion;
            TimeSpan duracions = viewModel.reserva.FechaHoraFin - viewModel.reserva.FechaHoraInicio;
            double horas = duracions.TotalHours;
            double costo = viewModel.moto.PrecioReserva / 24;
            double costoTotal = horas * costo;
            viewModel.duracion = duracions.Days+" Dia(s), "+ duracions.Hours + "Hora(s)";
            viewModel.reserva.CostoReserva = costoTotal;
            viewModel.ubicacion = ubicacion;
            return View(viewModel);
        }

        public IActionResult Pago(int usuario, string placa, DateTime fechaInicio, TimeSpan horaInicio, DateTime fechaFin, TimeSpan horaFin,
            int ubicacion, double costo, string comentario, string correo)
        {
            Reserva reserva = new Reserva
            {
                FKCedulaUsuario = usuario,
                FKPlacaMoto = placa,
                FechaInicio = fechaInicio,
                HoraInicio = horaInicio,
                FechaFin = fechaFin,
                HoraFin = horaFin,
                FKIDUbicacion = ubicacion,
                CostoReserva = costo,
                Comentario = comentario,
                CorreoPSE = correo
            };

            var viewModel = reservaService.crearReserva(reserva);
            return View();
        }
    }
}
