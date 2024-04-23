using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class RolUsuario
{
    public int IdrolUsuario { get; set; }

    public string NombreRol { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
