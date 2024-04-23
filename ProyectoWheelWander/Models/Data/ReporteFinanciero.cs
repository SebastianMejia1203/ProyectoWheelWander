using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class ReporteFinanciero
{
    public int IdreporteFinanciero { get; set; }

    public double Ingresos { get; set; }

    public double Gastos { get; set; }

    public DateTime FechaHoraRegistro { get; set; }

    public int Fkidfactura { get; set; }
}
