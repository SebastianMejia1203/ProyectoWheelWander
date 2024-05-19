namespace ProyectoWheelWander.Models.Data
{
    public class AdminMotoModel
    {
        public string PlacaMoto { get; set; }
        public string Urlfoto { get; set; } = ""!;
        public int EstadoReserva { get; set; }
        public int EstadoMoto { get; set; }
        public string NombreMarca { get; set; }
        public string ModeloMoto { get; set; }
    }
}
