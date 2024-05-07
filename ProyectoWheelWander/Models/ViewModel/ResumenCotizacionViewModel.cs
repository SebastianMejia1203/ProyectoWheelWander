using ProyectoWheelWander.Models.Data;

namespace ProyectoWheelWander.Models.ViewModel
{
    public class ResumenCotizacionViewModel
    {
        public Reserva reserva  { get; set; }
        public Moto? moto { get; set; }

        public string duracion { get; set; }

        public string ubicacion { get; set; }
    }
}
