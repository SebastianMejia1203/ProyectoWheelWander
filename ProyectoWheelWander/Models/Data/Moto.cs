using System;
using System.Collections.Generic;

namespace ProyectoWheelWander.Models.Data;

public partial class Moto
{
    public string PlacaMoto { get; set; } = null!;

    public int FkidmarcaMoto { get; set; }

    public int CilindrajeMoto { get; set; }

    public int FkidmotorMoto { get; set; }

    public int Potencia { get; set; }

    public int Torque { get; set; }

    public double Peso { get; set; }

    public int AlturaDelAsiento { get; set; }

    public int FkidtransmicionMoto { get; set; }

    public int FkidcombustibleMoto { get; set; }

    public double CapacidadCombustible { get; set; }

    public double ConsumoCombustible { get; set; }

    public int FkidfrenosMoto { get; set; }

    public int FkidsuspencionMoto { get; set; }

    public string? EquipamientoAdicional { get; set; }

    public string? InformacionAdicional { get; set; }

    public string Urlfoto { get; set; } = null!;

    public double PrecioReserva { get; set; }

    public byte EstadoReserva { get; set; }

    public byte EstadoMoto { get; set; }

    public int Fkidubicacion { get; set; }

    public int FkcedulaPropietario { get; set; }

    public string UrlfotoPlaca { get; set; } = null!;

    public string UrlfotoPapeles { get; set; } = null!;
}
