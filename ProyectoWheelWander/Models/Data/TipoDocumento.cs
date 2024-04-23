using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class TipoDocumento
{
    public int IdtipoDocumento { get; set; }

    public string NombreTipoDocumento { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
