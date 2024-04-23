using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class ModeloMoto
{
    public int IdmodeloMoto { get; set; }

    public string NombreModelo { get; set; } = null!;

    public int FkidmarcaMoto { get; set; }
}
