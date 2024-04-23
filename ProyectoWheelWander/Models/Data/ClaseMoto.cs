using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class ClaseMoto
{
    public int IdclaseMoto { get; set; }

    public string Fkidmoto { get; set; } = null!;

    public string NombreClase { get; set; } = null!;
}
