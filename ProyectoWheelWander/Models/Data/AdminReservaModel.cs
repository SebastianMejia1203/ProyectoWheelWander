namespace ProyectoWheelWander.Models.Data
{
    public class AdminReservaModel
    {
        public int IDReserva { get; set; }
        public string PlacaMoto { get; set; }
        public DateTime FechaInicio { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public TimeSpan HoraFin { get; set; }
        public double CostoReserva { get; set; }
        public string comentario { get; set; }
        public int IDUbicacion { get; set; }
        public string NombreUbicacion { get; set; }
        public string URLFoto { get; set; }
        public byte EstadoReserva { get; set; }
    }
}
