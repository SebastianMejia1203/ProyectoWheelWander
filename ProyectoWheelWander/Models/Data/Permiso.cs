using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoWheelWander.Models.Data
{
    [Table("Permisos")]
    public class Permiso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDPermiso { get; set; }

        [StringLength(45)]
        public string NombrePermiso { get; set; }

        [StringLength(255)]
        public string LinkPermiso { get; set; }
    }
}
