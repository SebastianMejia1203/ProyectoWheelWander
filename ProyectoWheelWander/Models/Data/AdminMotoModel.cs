namespace ProyectoWheelWander.Models.Data
{
    public class AdminMotoModel
    {
        public string PlacaMoto { get; set; }
        public int CilindrajeMoto { get; set; }
        public int Potencia { get; set; }
        public double Peso { get; set; }
        public double CapacidadCombustible { get; set; }
        public double ConsumoCombustible { get; set; }
        public string? InformacionAdicional { get; set; }
        public string Urlfoto { get; set; } = ""!;
        public double PrecioReserva { get; set; }
        public byte EstadoReserva { get; set; }
        public byte EstadoMoto { get; set; }
        public int ClaseMoto { get; set; }
        public string UrlfotoPlaca { get; set; } = ""!;
        public string Ubicacion { get; set; }
        public string NombreMarca { get; set; }
        public string ModeloMoto { get; set; }
        public string TipoTransmicion { get; set; }
    }
}
