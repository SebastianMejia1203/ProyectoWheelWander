using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Models;
using ProyectoWheelWander.Services;

namespace ProyectoWheelWander.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailServices _emailServices;
        public EmailController(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }

        [HttpPost]
        public IActionResult enviarEmail(string Para, string Asunto, string Contenido)
        {
            var request = new EmailDTO
            {
                Para = Para,
                Asunto = Asunto,
                Contenido = Contenido
            };
            request.Para = "itep.bqsoo27@juaxe.com";
            request.Asunto = "Reserva de moto";
            request.Contenido = "Se ha realizado una reserva de moto";
            _emailServices.enviarEmail(request);
            return Ok();
        }
    }
}
