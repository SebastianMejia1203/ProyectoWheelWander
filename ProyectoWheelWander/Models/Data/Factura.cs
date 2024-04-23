using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class Factura
{
    public int Idfactura { get; set; }

    public int Fkidreserva { get; set; }

    public double ValorPago { get; set; }

    public string ComprobanteTransaccion { get; set; } = null!;

    public DateOnly FechaTransaccion { get; set; }

    public byte EstadoTransaccion { get; set; }

    public double ValorComicion { get; set; }
}
