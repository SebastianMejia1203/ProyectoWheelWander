using ProyectoWheelWander.Models.Data;

namespace ProyectoWheelWander.Models
{
    public class RegisterViewModel
    {
        public Usuario Usuario { get; set; } // Modelo de usuario para registrar
        public List<TipoDocumento> TiposDocumento { get; set; } // Lista para el dropdown de tipos de documento
        public int? FKIDTipoDocumentoSelected { get; set; } // Para capturar el tipo de documento seleccionado
    }
}
