using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoWheelWander.Models.Data;

namespace ProyectoWheelWander.Models;

public partial class WheelWanderContext : DbContext
{
    public WheelWanderContext()
    {
    }

    public WheelWanderContext(DbContextOptions<WheelWanderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlertaUsuario> AlertaUsuarios { get; set; }

    public virtual DbSet<ClaseMoto> ClaseMotos { get; set; }

    public virtual DbSet<CombustibleMoto> CombustibleMotos { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FrenosMoto> FrenosMotos { get; set; }

    public virtual DbSet<HistorialReserva> HistorialReservas { get; set; }

    public virtual DbSet<MarcaMoto> MarcaMotos { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<ModeloMoto> ModeloMotos { get; set; }

    public virtual DbSet<Moto> Motos { get; set; }

    public virtual DbSet<MotorMoto> MotorMotos { get; set; }

    public virtual DbSet<ReporteFinanciero> ReporteFinancieros { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<RolUsuario> RolUsuarios { get; set; }

    public virtual DbSet<SuspencionMoto> SuspencionMotos { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TransmicionMoto> TransmicionMotos { get; set; }

    public virtual DbSet<Ubicacion> Ubicacions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:wheelwander.database.windows.net,1433;Initial Catalog=WheelWander;Persist Security Info=False;User ID=okaru;Password=Picapiedra2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlertaUsuario>(entity =>
        {
            entity.HasKey(e => e.IdalertaUsuario).HasName("PK__AlertaUs__D6E8B5C855A26EC5");

            entity.ToTable("AlertaUsuario");

            entity.Property(e => e.IdalertaUsuario).HasColumnName("IDAlertaUsuario");
            entity.Property(e => e.FechaHoraAlerta).HasColumnType("datetime");
            entity.Property(e => e.FkcedulaUsuario).HasColumnName("FKCedulaUsuario");
            entity.Property(e => e.MensajeAlerta)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TipoAlerta)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ClaseMoto>(entity =>
        {
            entity.HasKey(e => e.IdclaseMoto).HasName("PK__ClaseMot__452579D997165525");

            entity.ToTable("ClaseMoto");

            entity.Property(e => e.IdclaseMoto)
                .ValueGeneratedNever()
                .HasColumnName("IDClaseMoto");
            entity.Property(e => e.Fkidmoto)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("FKIDMoto");
            entity.Property(e => e.NombreClase)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CombustibleMoto>(entity =>
        {
            entity.HasKey(e => e.IdcombustibleMoto).HasName("PK__Combusti__CCFE73867BA39F52");

            entity.ToTable("CombustibleMoto");

            entity.Property(e => e.IdcombustibleMoto).HasColumnName("IDCombustibleMoto");
            entity.Property(e => e.TipoCombustible)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Idcomentario).HasName("PK__Comentar__46E6637E084BE8A6");

            entity.Property(e => e.Idcomentario).HasColumnName("IDComentario");
            entity.Property(e => e.Comentario1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Comentario");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Idfactura).HasName("PK__Factura__492FE9399F0C1BE7");

            entity.ToTable("Factura");

            entity.HasIndex(e => e.ComprobanteTransaccion, "UQ__Factura__63194D3E3B3483FD").IsUnique();

            entity.Property(e => e.Idfactura).HasColumnName("IDFactura");
            entity.Property(e => e.ComprobanteTransaccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fkidreserva).HasColumnName("FKIDReserva");
        });

        modelBuilder.Entity<FrenosMoto>(entity =>
        {
            entity.HasKey(e => e.IdfrenosMoto).HasName("PK__FrenosMo__8B05FFED77230673");

            entity.ToTable("FrenosMoto");

            entity.Property(e => e.IdfrenosMoto).HasColumnName("IDFrenosMoto");
            entity.Property(e => e.TipoFreno)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HistorialReserva>(entity =>
        {
            entity.HasKey(e => e.IdhistorialReserva).HasName("PK__Historia__36CC8A85DB440555");

            entity.ToTable("HistorialReserva");

            entity.Property(e => e.IdhistorialReserva).HasColumnName("IDHistorialReserva");
            entity.Property(e => e.Fkidfactura).HasColumnName("FKIDFactura");
        });

        modelBuilder.Entity<MarcaMoto>(entity =>
        {
            entity.HasKey(e => e.IdmarcaMoto).HasName("PK__MarcaMot__6D0B21C6497C4A5D");

            entity.ToTable("MarcaMoto");

            entity.Property(e => e.IdmarcaMoto).HasColumnName("IDMarcaMoto");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdmetodoPago).HasName("PK__MetodoPa__8D99F338F08F1ABF");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.IdmetodoPago).HasColumnName("IDMetodoPago");
            entity.Property(e => e.EstadoMetodoPago).HasDefaultValue((byte)1);
            entity.Property(e => e.InformacionMetodoPago)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TipoMetodoPago)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ModeloMoto>(entity =>
        {
            entity.HasKey(e => e.IdmodeloMoto).HasName("PK__ModeloMo__8AA67608EEADE4E6");

            entity.ToTable("ModeloMoto");

            entity.Property(e => e.IdmodeloMoto).HasColumnName("IDModeloMoto");
            entity.Property(e => e.FkidmarcaMoto).HasColumnName("FKIDMarcaMoto");
            entity.Property(e => e.NombreModelo)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Moto>(entity =>
        {
            entity.HasKey(e => e.PlacaMoto).HasName("PK__Moto__140A1C5DCB8425CC");

            entity.ToTable("Moto");

            entity.Property(e => e.PlacaMoto)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.EquipamientoAdicional)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EstadoReserva).HasDefaultValue((byte)1);
            entity.Property(e => e.FkcedulaPropietario).HasColumnName("FKCedulaPropietario");
            entity.Property(e => e.FkidcombustibleMoto).HasColumnName("FKIDCombustibleMoto");
            entity.Property(e => e.FkidfrenosMoto).HasColumnName("FKIDFrenosMoto");
            entity.Property(e => e.FkidmarcaMoto).HasColumnName("FKIDMarcaMoto");
            entity.Property(e => e.FkidmotorMoto).HasColumnName("FKIDMotorMoto");
            entity.Property(e => e.FkidsuspencionMoto).HasColumnName("FKIDSuspencionMoto");
            entity.Property(e => e.FkidtransmicionMoto).HasColumnName("FKIDTransmicionMoto");
            entity.Property(e => e.Fkidubicacion).HasColumnName("FKIDUbicacion");
            entity.Property(e => e.InformacionAdicional)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Urlfoto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URLFoto");
            entity.Property(e => e.UrlfotoPapeles)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URLFotoPapeles");
            entity.Property(e => e.UrlfotoPlaca)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URLFotoPlaca");
        });

        modelBuilder.Entity<MotorMoto>(entity =>
        {
            entity.HasKey(e => e.IdmotorMoto).HasName("PK__MotorMot__94E82215B186F47A");

            entity.ToTable("MotorMoto");

            entity.Property(e => e.IdmotorMoto).HasColumnName("IDMotorMoto");
            entity.Property(e => e.TipoMotor)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ReporteFinanciero>(entity =>
        {
            entity.HasKey(e => e.IdreporteFinanciero).HasName("PK__ReporteF__F30698C9D3D468C4");

            entity.ToTable("ReporteFinanciero");

            entity.Property(e => e.IdreporteFinanciero).HasColumnName("IDReporteFinanciero");
            entity.Property(e => e.FechaHoraRegistro).HasColumnType("datetime");
            entity.Property(e => e.Fkidfactura).HasColumnName("FKIDFactura");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Idreserva).HasName("PK__Reserva__D9F2FA676BB87125");

            entity.ToTable("Reserva");

            entity.Property(e => e.Idreserva).HasColumnName("IDReserva");
            entity.Property(e => e.FechaHoraFin).HasColumnType("datetime");
            entity.Property(e => e.FechaHoraInicio).HasColumnType("datetime");
            entity.Property(e => e.FkcedulaUsuario).HasColumnName("FKCedulaUsuario");
            entity.Property(e => e.FkidclaseMoto).HasColumnName("FKIDClaseMoto");
            entity.Property(e => e.Fkidcomentario).HasColumnName("FKIDComentario");
            entity.Property(e => e.FkidmetodoPago).HasColumnName("FKIDMetodoPago");
            entity.Property(e => e.Fkidubicacion).HasColumnName("FKIDUbicacion");
            entity.Property(e => e.FkplacaMoto)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("FKPlacaMoto");
        });

        modelBuilder.Entity<RolUsuario>(entity =>
        {
            entity.HasKey(e => e.IdrolUsuario).HasName("PK__RolUsuar__C2954FF14046C144");

            entity.ToTable("RolUsuario");

            entity.Property(e => e.IdrolUsuario).HasColumnName("IDRolUsuario");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SuspencionMoto>(entity =>
        {
            entity.HasKey(e => e.IdsuspencionMoto).HasName("PK__Suspenci__3ED350ECED5BE366");

            entity.ToTable("SuspencionMoto");

            entity.Property(e => e.IdsuspencionMoto).HasColumnName("IDSuspencionMoto");
            entity.Property(e => e.TipoSuspencion)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdtipoDocumento).HasName("PK__TipoDocu__429583F1BBC09128");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.IdtipoDocumento).HasColumnName("IDTipoDocumento");
            entity.Property(e => e.NombreTipoDocumento)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TransmicionMoto>(entity =>
        {
            entity.HasKey(e => e.IdtransmicionMoto).HasName("PK__Transmic__2EDC32C03111455D");

            entity.ToTable("TransmicionMoto");

            entity.Property(e => e.IdtransmicionMoto).HasColumnName("IDTransmicionMoto");
            entity.Property(e => e.TipoTransmicion)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ubicacion>(entity =>
        {
            entity.HasKey(e => e.Idubicacion).HasName("PK__Ubicacio__B4CA90FCE670E261");

            entity.ToTable("Ubicacion");

            entity.Property(e => e.Idubicacion).HasColumnName("IDUbicacion");
            entity.Property(e => e.NombreUbicacion)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Cedula).HasName("PK__usuario__B4ADFE395CA26F4A");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Celular, "UQ__usuario__0E9B6C3B442205F1").IsUnique();

            entity.HasIndex(e => e.Contrasena, "UQ__usuario__A96DE1515FB6175A").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__usuario__A9D10534E39BD0BD").IsUnique();

            entity.Property(e => e.Cedula).ValueGeneratedNever();
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoUsuario).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FkidtipoDocumento).HasColumnName("FKIDTipoDocumento");
            entity.Property(e => e.Idrol)
                .HasDefaultValue(2)
                .HasColumnName("IDRol");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UrlfotoFcedula)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URLFotoFCedula");
            entity.Property(e => e.UrlfotoFlicencia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URLFotoFLicencia");
            entity.Property(e => e.UrlfotoPcedula)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URLFotoPCedula");
            entity.Property(e => e.UrlfotoPlicencia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URLFotoPLicencia");

            entity.HasOne(d => d.FkidtipoDocumentoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FkidtipoDocumento)
                .HasConstraintName("FK_usuario_TipoDocumento");

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idrol)
                .HasConstraintName("FK_usuario_RolUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
