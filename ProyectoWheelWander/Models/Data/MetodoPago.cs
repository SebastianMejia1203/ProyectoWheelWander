using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class MetodoPago
{
    public int IdmetodoPago { get; set; }

    public string TipoMetodoPago { get; set; } = null!;

    public byte EstadoMetodoPago { get; set; }

    public string InformacionMetodoPago { get; set; } = null!;
}
