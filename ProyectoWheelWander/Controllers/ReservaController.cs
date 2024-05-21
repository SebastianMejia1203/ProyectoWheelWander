using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Datos;
using ProyectoWheelWander.Models.Data;
using ProyectoWheelWander.Models.ViewModel;
using System.Security.Claims;
using System.IO;
using System.Threading.Tasks;
using iText.Html2pdf;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using ProyectoWheelWander.Services;
using ProyectoWheelWander.Models;


namespace ProyectoWheelWander.Controllers
{
    public class ReservaController : Controller
    {

        private readonly IWebHostEnvironment _host;


        MotoDatos motoService = new MotoDatos();
        ReservaDatos reservaService = new ReservaDatos();

        public ReservaController(IWebHostEnvironment host)
        {
            _host = host;
        }

        public IActionResult DescargarPDF(int cedula) 
        {
            var cedulaAutenticada = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            int ced = Convert.ToInt32(cedulaAutenticada);
            PDFViewModel pdfInfo = reservaService.PDFInfo(ced);

            DateTime FechaHoraInicio = pdfInfo.FechaInicio + pdfInfo.HoraInicio;
            DateTime FechaHoraFin = pdfInfo.FechaFin + pdfInfo.HoraFin;
            TimeSpan duracions = FechaHoraFin - FechaHoraInicio;
            double horas = duracions.TotalHours;
            var duracion = duracions.Days + " Dia(s), " + duracions.Hours + "Hora(s)";
            var costo = pdfInfo.CostoReserva - (pdfInfo.CostoReserva * 0.1);
            var comision = pdfInfo.CostoReserva * 0.1;

            var data = Document.Create(pdf =>
            {

                pdf.Page(page =>
                {
                    page.Margin(20);

                    page.Header().ShowOnce().Row(row =>
                    {
                        var rutaImagen = Path.Combine(_host.WebRootPath, "Images/logo2.png");
                        byte[] ImageData = System.IO.File.ReadAllBytes(rutaImagen);

                        //row.ConstantItem(555).Height(80).Placeholder();
                        row.ConstantItem(555).Image(ImageData);
                        row.RelativeItem().PaddingLeft(-850).PaddingTop(95).Column(column =>
                        {
                            column.Item().PaddingRight(5).Text("Calle 5 N. 7-47, Cartagena del Chairá, Caquetá").AlignCenter();
                            column.Item().PaddingRight(5).Text("NIT. 1234546780-1").Bold().AlignCenter();
                            column.Item().PaddingRight(5).Text("Tel +57 302 620 3948").AlignCenter();
                        });
                        row.RelativeItem().PaddingLeft(-300).PaddingTop(95).Column(column =>
                        {
                            column.Item().Border(1).BorderColor("#F3B61F").PaddingRight(5).Text("Reserva de Motocicleta").FontSize(12).AlignCenter();
                            column.Item().Border(1).BorderColor("#F3B61F").Background("#F3B61F").PaddingRight(5).Text("N. "+ pdfInfo.IDReserva).FontSize(14).AlignCenter().Bold().FontColor("#FFF");
                            column.Item().Border(1).BorderColor("#F3B61F").PaddingRight(5).Text("Fecha: "+ pdfInfo.FechaCreacion).AlignCenter();
                        });
                    });

                    page.Content().PaddingVertical(20).Column(col =>
                    {
                        col.Item().Text("Datos del cliente").Underline().Bold().FontSize(14);
                        col.Item().Text(txt =>
                        {
                            txt.Span("Nombre: ").SemiBold().FontSize(10);
                            txt.Span(pdfInfo.PrimerNombre +" "+ pdfInfo.SegundoNombre+ " " + pdfInfo.PrimerApellido+ " " + pdfInfo.SegundoApellido).FontSize(10);
                        });
                        col.Item().Text(txt =>
                        {
                            txt.Span("Cédula: ").SemiBold().FontSize(10);
                            txt.Span(pdfInfo.FKCedulaUsuario+"").FontSize(10);
                        });
                        col.Item().Text(txt =>
                        {
                            txt.Span("Celular: ").SemiBold().FontSize(10);
                            txt.Span(pdfInfo.Celular+"").FontSize(10);
                        });
                        col.Item().PaddingTop(5).LineHorizontal(0.5f);

                        col.Item().PaddingTop(10).Text("Datos de la reserva").Underline().Bold().FontSize(14);

                        col.Item().PaddingTop(5).Width(130).Background("#F3B61F").Padding(2).Text("Tiempo cotizado").FontColor("#FFF").Bold().FontSize(13);
                        col.Item().Width(130).Background("#F3B61F").Padding(2).Text("Fecha y hora de inicio").FontColor("#FFF").Bold().FontSize(13);
                        col.Item().Width(130).Background("#F3B61F").Padding(2).Text("Fecha y hora de fin").FontColor("#FFF").Bold().FontSize(13);
                        col.Item().Width(130).Background("#F3B61F").Padding(2).Text("Costo de la reserva").FontColor("#FFF").Bold().FontSize(13);
                        col.Item().Width(130).Background("#F3B61F").Padding(2).Text("Comisión").FontColor("#FFF").Bold().FontSize(13);
                        col.Item().Width(130).Background("#F3B61F").Padding(2).Text("Ubicación").FontColor("#FFF").Bold().FontSize(13);
                        col.Item().PaddingTop(10).Width(130).Background("#F3B61F").Padding(2).Text("Placa de la moto: ").FontColor("#FFF").Bold().FontSize(13);
                        col.Item().Width(130).Background("#F3B61F").Padding(2).Text("Propietario: ").FontColor("#FFF").Bold().FontSize(13);

                        col.Item().PaddingTop(-170).PaddingLeft(135).Width(150).Padding(2).Text(duracion);
                        col.Item().PaddingTop(2).PaddingLeft(135).Width(150).Padding(2).Text(FechaHoraInicio);
                        col.Item().PaddingTop(2).PaddingLeft(135).Width(150).Padding(2).Text(FechaHoraFin);
                        col.Item().PaddingTop(2).PaddingLeft(135).Width(150).Padding(2).Text("$" + costo);
                        col.Item().PaddingTop(2).PaddingLeft(135).Width(150).Padding(2).Text("$" + comision);
                        col.Item().PaddingTop(2).PaddingLeft(135).Width(150).Padding(2).Text(pdfInfo.Ubicacion);
                        col.Item().PaddingTop(12).PaddingLeft(135).Width(150).Padding(2).Text(pdfInfo.FKPlacaMoto);
                        col.Item().PaddingTop(2).PaddingLeft(135).Width(150).Padding(2).Text(pdfInfo.FKCedulaUsuario);

                        col.Item().PaddingTop(-170).PaddingLeft(300).Width(220).Height(170).Placeholder();

                        col.Item().PaddingTop(15).LineHorizontal(0.5f);

                        col.Item().PaddingTop(10).Text("Información de pago").Underline().Bold().FontSize(14);
                        col.Item().PaddingTop(10).Text(txt =>
                        {
                            txt.Span("Método de pago: ").SemiBold();
                            txt.Span("PSE");
                        });
                        col.Item().Text(txt =>
                        {
                            txt.Span("Estado de la transacción: ").SemiBold();
                            txt.Span("APROVADO").BackgroundColor("#56a608").FontColor("#fff");
                        });
                        col.Item().Text(txt =>
                        {
                            txt.Span("Correo electronico: ").SemiBold();
                            txt.Span(pdfInfo.CorreoPSE);
                        });
                        col.Item().Text(txt =>
                        {
                            txt.Span("Total pagado: ").SemiBold();
                            txt.Span("$"+pdfInfo.CostoReserva);
                        });
                    });

                    page.Footer().AlignRight().Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });
            }).GeneratePdf();

            Stream stream = new MemoryStream(data);
            return File(stream, "application/pdf", "detalleReserva.pdf");
        }


        // GET: Reserva
        [Authorize]
        public ActionResult Index()
        {
            var cedulaAutenticada = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            int cedula = Convert.ToInt32(cedulaAutenticada);
            var view = motoService.reservaPorUsuario(cedula);

            return View(view);
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
            if (viewModel)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ErrorPago");
            }
        }

        public IActionResult Detalle(string placa, int estado)
        {
            var cedulaAutenticada = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            int cedula = Convert.ToInt32(cedulaAutenticada);
            var view = new AdminReservaViewModel { 
                listaReservas = motoService.reservaPorUsuario(cedula),
                estados = estado
            };

            return View(view);
        }

        public IActionResult cancelarReserva(int idReserva)
        {
            var respuesta = reservaService.cancelarReserva(idReserva);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
