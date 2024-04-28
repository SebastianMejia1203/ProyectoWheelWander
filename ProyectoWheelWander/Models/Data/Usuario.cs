using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWheelWander.Models.Data;

[Table("usuario")]
public partial class Usuario
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // Indica que la cédula es asignada manualmente y no generada por la base de datos.
    public long Cedula { get; set; }

    [Required]
    [StringLength(20)]
    public string PrimerNombre { get; set; }

    [StringLength(20)]
    public string? SegundoNombre { get; set; }

    [Required]
    [StringLength(20)]
    public string PrimerApellido { get; set; }

    [StringLength(20)]
    public string? SegundoApellido { get; set; }

    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Contrasena { get; set; }

    [Required]
    public long Celular { get; set; }

    [Required]
    public byte EstadoUsuario { get; set; } = 1;

    [Required]
    public int IDRol { get; set; } = 2;

    [Required]
    public DateTime FechaRegistro { get; set; }

    [Required]
    public int FKIDTipoDocumento { get; set; }

    [Required]
    [StringLength(255)]
    public string? URLFotoFCedula { get; set; }

    [Required]
    [StringLength(255)]
    public string? URLFotoPCedula { get; set; }

    [Required]
    [StringLength(255)]
    public string? URLFotoFLicencia { get; set; }

    [Required]
    [StringLength(255)]
    public string? URLFotoPLicencia { get; set; }

    [Required]
    public DateTime FechaNacimiento { get; set; }
}
