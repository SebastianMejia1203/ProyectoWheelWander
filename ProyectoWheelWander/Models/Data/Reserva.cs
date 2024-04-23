using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class Reserva
{
    public int Idreserva { get; set; }

    public int FkcedulaUsuario { get; set; }

    public string FkplacaMoto { get; set; } = null!;

    public DateTime FechaHoraInicio { get; set; }

    public DateTime FechaHoraFin { get; set; }

    public int FkidmetodoPago { get; set; }

    public int Fkidubicacion { get; set; }

    public int FkidclaseMoto { get; set; }

    public double CostoReserva { get; set; }

    public int Fkidcomentario { get; set; }
}
