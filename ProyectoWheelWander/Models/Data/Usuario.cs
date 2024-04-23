using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class Usuario
{
    public long Cedula { get; set; }

    public string? PrimerNombre { get; set; }

    public string? SegundoNombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Contrasena { get; set; } = string.Empty;

    public long? Celular { get; set; }

    public byte? EstadoUsuario { get; set; }

    public byte? Sexo { get; set; }

    public int? Idrol { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public int? FkidtipoDocumento { get; set; }

    public string? UrlfotoFcedula { get; set; }

    public string? UrlfotoPcedula { get; set; }

    public string? UrlfotoFlicencia { get; set; }

    public string? UrlfotoPlicencia { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public virtual TipoDocumento? FkidtipoDocumentoNavigation { get; set; }

    public virtual RolUsuario? IdrolNavigation { get; set; }
}
