using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoWheelWander.Models.Data
{
    [Table("RolPermisos")]
    public class RolPermiso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDRolPermiso { get; set; }

        public int? FKIDRol { get; set; }
        public int? FKIDPermiso { get; set; }

        [ForeignKey("FKIDRol")]
        public virtual RolUsuario RolUsuario { get; set; }

        [ForeignKey("FKIDPermiso")]
        public virtual Permiso Permiso { get; set; }
    }
}
