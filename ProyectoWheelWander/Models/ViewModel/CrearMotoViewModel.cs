using ProyectoWheelWander.Models.Data;

namespace ProyectoWheelWander.Models.ViewModel
{
    public class CrearMotoViewModel
    {
        public Moto moto { get; set; }
        public List<Ubicacion> ubicaciones { get; set; }
        public List<TransmicionMoto> transmiciones { get; set; }
        public List<MarcaMoto> marcas { get; set;}
    }
}
