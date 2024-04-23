using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class Comentario
{
    public int Idcomentario { get; set; }

    public string Comentario1 { get; set; } = null!;

    public int Valoracion { get; set; }
}
