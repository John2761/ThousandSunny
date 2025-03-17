using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Reserva.Infraestructure.Models;

namespace Reserva.Infraestructure.Data;

public partial class ThousandSunnyContext : DbContext
{
    public ThousandSunnyContext(DbContextOptions<ThousandSunnyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Barco> Barco { get; set; }

    public virtual DbSet<BarcoHabitacion> BarcoHabitacion { get; set; }

    public virtual DbSet<Complemento> Complemento { get; set; }

    public virtual DbSet<Crucero> Crucero { get; set; }

    public virtual DbSet<DatosPago> DatosPago { get; set; }

    public virtual DbSet<Destino> Destino { get; set; }

    public virtual DbSet<DetalleReservacion> DetalleReservacion { get; set; }

    public virtual DbSet<Fecha> Fecha { get; set; }

    public virtual DbSet<Habitacion> Habitacion { get; set; }

    public virtual DbSet<Huesped> Huesped { get; set; }

    public virtual DbSet<Itinerario> Itinerario { get; set; }

    public virtual DbSet<Precio> Precio { get; set; }

    public virtual DbSet<Puerto> Puerto { get; set; }

    public virtual DbSet<ReservaComplemento> ReservaComplemento { get; set; }

    public virtual DbSet<Reservacion> Reservacion { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<TipoPago> TipoPago { get; set; }

    public virtual DbSet<TransaccionPago> TransaccionPago { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Barco>(entity =>
        {
            entity.HasKey(e => e.IdBarco).HasName("PK__Barco__7DAFC779033053A8");

            entity.Property(e => e.IdBarco)
                .ValueGeneratedNever()
                .HasColumnName("idBarco");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BarcoHabitacion>(entity =>
        {
            entity.HasKey(e => new { e.IdBarco, e.IdHabitacion }).HasName("PK__BarcoHab__403294C7D1233704");

            entity.Property(e => e.IdBarco).HasColumnName("idBarco");
            entity.Property(e => e.IdHabitacion).HasColumnName("idHabitacion");

            entity.HasOne(d => d.BarcoNavigation).WithMany(p => p.BarcoHabitacion)
                .HasForeignKey(d => d.IdBarco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BarcoHabi__idBar__34C8D9D1");

            entity.HasOne(d => d.HabitacionNavigation).WithMany(p => p.BarcoHabitacion)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BarcoHabi__idHab__35BCFE0A");
        });

        modelBuilder.Entity<Complemento>(entity =>
        {
            entity.HasKey(e => e.IdComplemento).HasName("PK__Compleme__A0F508D3041FE115");

            entity.Property(e => e.IdComplemento)
                .ValueGeneratedNever()
                .HasColumnName("idComplemento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Crucero>(entity =>
        {
            entity.HasKey(e => e.IdCrucero).HasName("PK__Crucero__68CB5604D6D59CCF");

            entity.Property(e => e.IdCrucero)
                .ValueGeneratedNever()
                .HasColumnName("idCrucero");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IdBarco).HasColumnName("idBarco");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBarcoNavigation).WithMany(p => p.Crucero)
                .HasForeignKey(d => d.IdBarco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Crucero__idBarco__38996AB5");
        });

        modelBuilder.Entity<DatosPago>(entity =>
        {
            entity.HasKey(e => e.IdDatosPago).HasName("PK__DatosPag__51596242F51B2AB6");

            entity.Property(e => e.IdDatosPago)
                .ValueGeneratedNever()
                .HasColumnName("idDatosPago");
            entity.Property(e => e.IdTipoPago).HasColumnName("idTipoPago");
            entity.Property(e => e.MontoPendiente).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MontoPrima).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.DatosPago)
                .HasForeignKey(d => d.IdTipoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DatosPago__idTip__4222D4EF");
        });

        modelBuilder.Entity<Destino>(entity =>
        {
            entity.HasKey(e => e.IdDestino).HasName("PK__Destino__87E69F08163DEF27");

            entity.Property(e => e.IdDestino)
                .ValueGeneratedNever()
                .HasColumnName("idDestino");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetalleReservacion>(entity =>
        {
            entity.HasKey(e => e.IdDetalleRes).HasName("PK__DetalleR__F2234F286F847F9C");

            entity.Property(e => e.IdDetalleRes)
                .ValueGeneratedNever()
                .HasColumnName("idDetalleRes");
            entity.Property(e => e.IdHabitacion).HasColumnName("idHabitacion");
            entity.Property(e => e.IdReservacion).HasColumnName("idReservacion");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.DetalleReservacion)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleRe__idHab__4D94879B");

            entity.HasOne(d => d.IdReservacionNavigation).WithMany(p => p.DetalleReservacion)
                .HasForeignKey(d => d.IdReservacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleRe__idRes__4E88ABD4");
        });

        modelBuilder.Entity<Fecha>(entity =>
        {
            entity.HasKey(e => e.IdFecha).HasName("PK__Fecha__65F2E869751960E2");

            entity.Property(e => e.IdFecha)
                .ValueGeneratedNever()
                .HasColumnName("idFecha");
            entity.Property(e => e.FechaSalida).HasColumnType("datetime");
            entity.Property(e => e.IdCrucero).HasColumnName("idCrucero");

            entity.HasOne(d => d.IdCruceroNavigation).WithMany(p => p.Fecha)
                .HasForeignKey(d => d.IdCrucero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fecha__idCrucero__3B75D760");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__D9D53BE26FD62DD3");

            entity.Property(e => e.IdHabitacion).HasColumnName("idHabitacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tamaño)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Huesped>(entity =>
        {
            entity.HasKey(e => e.IdHuesped).HasName("PK__Huesped__4B73CF97CE3AEDCB");

            entity.Property(e => e.IdHuesped)
                .ValueGeneratedNever()
                .HasColumnName("idHuesped");
            entity.Property(e => e.Apellido1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdReservacion).HasColumnName("idReservacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdReservacionNavigation).WithMany(p => p.Huesped)
                .HasForeignKey(d => d.IdReservacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Huesped__idReser__4AB81AF0");
        });

        modelBuilder.Entity<Itinerario>(entity =>
        {
            entity.HasKey(e => e.IdItinerario).HasName("PK__Itinerar__B201E2D67E9A1AE2");

            entity.Property(e => e.IdItinerario)
                .ValueGeneratedNever()
                .HasColumnName("idItinerario");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IdCrucero).HasColumnName("idCrucero");
            entity.Property(e => e.IdPuerto).HasColumnName("idPuerto");

            entity.HasOne(d => d.IdCruceroNavigation).WithMany(p => p.Itinerario)
                .HasForeignKey(d => d.IdCrucero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Itinerari__idCru__3F466844");

            entity.HasOne(d => d.IdPuertoNavigation).WithMany(p => p.Itinerario)
                .HasForeignKey(d => d.IdPuerto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Itinerari__idPue__3E52440B");
        });

        modelBuilder.Entity<Precio>(entity =>
        {
            entity.HasKey(e => e.IdPrecio).HasName("PK__Precio__BF8B120C2B2433B4");

            entity.Property(e => e.IdPrecio)
                .ValueGeneratedNever()
                .HasColumnName("idPrecio");
            entity.Property(e => e.IdFecha).HasColumnName("idFecha");
            entity.Property(e => e.IdHabitacion).HasColumnName("idHabitacion");
            entity.Property(e => e.PrecioHabitacion).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdFechaNavigation).WithMany(p => p.Precio)
                .HasForeignKey(d => d.IdFecha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Precio__idFecha__59FA5E80");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Precio)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Precio__idHabita__5AEE82B9");
        });

        modelBuilder.Entity<Puerto>(entity =>
        {
            entity.HasKey(e => e.IdPuerto).HasName("PK__Puerto__ADB48910746630F4");

            entity.Property(e => e.IdPuerto)
                .ValueGeneratedNever()
                .HasColumnName("idPuerto");
            entity.Property(e => e.IdDestino).HasColumnName("idDestino");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDestinoNavigation).WithMany(p => p.Puerto)
                .HasForeignKey(d => d.IdDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Puerto__idDestin__267ABA7A");
        });

        modelBuilder.Entity<ReservaComplemento>(entity =>
        {
            entity.HasKey(e => e.IdResCom).HasName("PK__ReservaC__7933A5B96DCEB936");

            entity.Property(e => e.IdResCom)
                .ValueGeneratedNever()
                .HasColumnName("idResCom");
            entity.Property(e => e.IdComplemento).HasColumnName("idComplemento");
            entity.Property(e => e.IdReservacion).HasColumnName("idReservacion");

            entity.HasOne(d => d.IdComplementoNavigation).WithMany(p => p.ReservaComplemento)
                .HasForeignKey(d => d.IdComplemento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReservaCo__idCom__5441852A");

            entity.HasOne(d => d.IdReservacionNavigation).WithMany(p => p.ReservaComplemento)
                .HasForeignKey(d => d.IdReservacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReservaCo__idRes__534D60F1");
        });

        modelBuilder.Entity<Reservacion>(entity =>
        {
            entity.HasKey(e => e.IdReservacion).HasName("PK__Reservac__C813D8AD774F8C48");

            entity.Property(e => e.IdReservacion)
                .ValueGeneratedNever()
                .HasColumnName("idReservacion");
            entity.Property(e => e.FechaLimite).HasColumnType("datetime");
            entity.Property(e => e.FechaReserva).HasColumnType("datetime");
            entity.Property(e => e.IdCrucero).HasColumnName("idCrucero");
            entity.Property(e => e.IdDatosPago).HasColumnName("idDatosPago");
            entity.Property(e => e.IdFecha).HasColumnName("idFecha");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IdCruceroNavigation).WithMany(p => p.Reservacion)
                .HasForeignKey(d => d.IdCrucero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__idCru__47DBAE45");

            entity.HasOne(d => d.IdDatosPagoNavigation).WithMany(p => p.Reservacion)
                .HasForeignKey(d => d.IdDatosPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__idDat__45F365D3");

            entity.HasOne(d => d.IdFechaNavigation).WithMany(p => p.Reservacion)
                .HasForeignKey(d => d.IdFecha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__idFec__44FF419A");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservacion)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__idUsu__46E78A0C");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F767345A85D");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("idRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__TipoPago__AC5BA85BEA200FE2");

            entity.Property(e => e.IdTipoPago)
                .ValueGeneratedNever()
                .HasColumnName("idTipoPago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TransaccionPago>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PK__Transacc__5B8761F09D1FF0B6");

            entity.Property(e => e.IdTransaccion)
                .ValueGeneratedNever()
                .HasColumnName("idTransaccion");
            entity.Property(e => e.CodigoAutorizacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdReservacion).HasColumnName("idReservacion");

            entity.HasOne(d => d.IdReservacionNavigation).WithMany(p => p.TransaccionPago)
                .HasForeignKey(d => d.IdReservacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__idRes__571DF1D5");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A686A27816");

            entity.HasIndex(e => e.Correo, "UQ__Usuario__60695A1963D3BF57").IsUnique();

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("idUsuario");
            entity.Property(e => e.Apellido1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__idRol__2C3393D0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
