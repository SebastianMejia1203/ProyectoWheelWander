using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoWheelWander.Models.Data;

public partial class Reserva
{
    [Key]
    public int IDReserva { get; set; }

    public int FKCedulaUsuario { get; set; }

    public string FKPlacaMoto { get; set; } = null!;

    public DateTime FechaHoraInicio { get; set; }

    public DateTime FechaHoraFin { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }
    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFin { get; set; }

    public string CorreoPSE { get; set; }

    public int FKIDUbicacion { get; set; }

    public double CostoReserva { get; set; }

    public string Comentario { get; set; }
}
