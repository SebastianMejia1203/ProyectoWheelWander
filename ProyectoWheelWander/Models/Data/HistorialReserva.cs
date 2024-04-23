using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class HistorialReserva
{
    public int IdhistorialReserva { get; set; }

    public int Fkidfactura { get; set; }

    public DateOnly FechaTransaccion { get; set; }
}
