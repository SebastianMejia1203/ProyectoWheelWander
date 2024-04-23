using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class AlertaUsuario
{
    public int IdalertaUsuario { get; set; }

    public string TipoAlerta { get; set; } = null!;

    public DateTime FechaHoraAlerta { get; set; }

    public int FkcedulaUsuario { get; set; }

    public string MensajeAlerta { get; set; } = null!;
}
