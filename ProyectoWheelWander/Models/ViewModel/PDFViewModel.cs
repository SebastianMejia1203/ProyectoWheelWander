using ProyectoWheelWander.Models.Data;

namespace ProyectoWheelWander.Models.ViewModel
{
    public class PDFViewModel
    {
        public int IDReserva { get; set; }
        public int FKCedulaUsuario { get; set; }
        public string FKPlacaMoto { get; set; } = null!;
        public string CorreoPSE { get; set; } = string.Empty;
        public double CostoReserva { get; set; }
        public DateTime FechaInicio { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public TimeSpan HoraFin { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public int  EstadoReserva { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public string PrimerNombre { get; set; } = string.Empty;
        public string SegundoNombre { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        public long Celular { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public string URLFoto { get; set; } = string.Empty;
    }
}
