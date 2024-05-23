using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWheelWander.Models.Data;

[Table("Usuarios")]
public partial class Usuario
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // Indica que la cédula es asignada manualmente y no generada por la base de datos.
    public int Cedula { get; set; }

    [Required]
    [StringLength(20)]
    public string PrimerNombre { get; set; }

    [StringLength(20)]
    public string SegundoNombre { get; set; }

    [Required]
    [StringLength(20)]
    public string PrimerApellido { get; set; }

    [StringLength(20)]
    public string SegundoApellido { get; set; }

    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(50)]
    public string Contrasena { get; set; }

    [Required]
    public long Celular { get; set; }

    [Required]
    [Range(0, 1)]
    public byte EstadoUsuario { get; set; } = 1;

    [Required]
    public DateTime FechaRegistro { get; set; } = DateTime.Now.Date;

    public int? FKIDTipoDocumento { get; set; }

    [Required]
    public DateTime FechaNacimiento { get; set; }

    [Range(0, double.MaxValue)]
    public double Ganancias { get; set; } = 0;

    [StringLength(255)]
    public string Foto { get; set; } = "";

    [ForeignKey("FKIDTipoDocumento")]
    public virtual string TipoDocumento { get; set; }
}
