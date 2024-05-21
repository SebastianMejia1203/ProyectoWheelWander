using ProyectoWheelWander.Models.Data;

namespace ProyectoWheelWander.Models.ViewModel
{
    public class AdminMotoViewModel
    {
        public List<AdminMotoModel> listaMotos { get; set; }

        public List<historialViewModel> historial { get; set; }

        public Reserva reserva { get; set; }

        public TimeSpan tiempo { get; set; }

        public string duracion { get; set; }

        public double gananciaActual { get; set; }
        public double gananciaHistorica { get; set; }
    }
}
