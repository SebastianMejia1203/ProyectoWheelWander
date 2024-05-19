using ProyectoWheelWander.Models.Data;
using ProyectoWheelWander.Models.ViewModel;

namespace ProyectoWheelWander.Models.ViewModel
{
    public class CatalogoViewModel
    {
        public List<CatalogoModel> motosCatalogo { get; set; }
        public List<Ubicacion> ubicaciones { get; set; }
        public List<MarcaMoto> marcas { get; set; }

        // Propiedades para mantener los estados de los filtros
        public int UbicacionSeleccionada { get; set; }
        public int? MarcaSeleccionada { get; set; }
        public int? ClaseSeleccionada { get; set; }
        public DateTime? FechaInicio { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public int? OrdenarPorNombre { get; set; }
        public int? OrdenarPorPrecio { get; set; }

        public ResumenCotizacionViewModel? resumen { get; set; }
    }
}
