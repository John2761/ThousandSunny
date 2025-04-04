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
            entity.HasKey(e => e.IdBarco).HasName("PK__Barco__7DAFC7796102BDED");

            entity.Property(e => e.IdBarco).HasColumnName("idBarco");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BarcoHabitacion>(entity =>
        {
            entity.HasKey(e => new { e.IdBarco, e.IdHabitacion }).HasName("PK__BarcoHab__403294C73CAE6456");

            entity.Property(e => e.IdBarco).HasColumnName("idBarco");
            entity.Property(e => e.IdHabitacion).HasColumnName("idHabitacion");

            entity.HasOne(d => d.IdBarcoNavigation).WithMany(p => p.BarcoHabitacion)
                .HasForeignKey(d => d.IdBarco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BarcoHabi__idBar__34C8D9D1");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.BarcoHabitacion)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BarcoHabi__idHab__35BCFE0A");
        });

        modelBuilder.Entity<Complemento>(entity =>
        {
            entity.HasKey(e => e.IdComplemento).HasName("PK__Compleme__A0F508D312EA8A14");

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
            entity.HasKey(e => e.IdCrucero).HasName("PK__Crucero__68CB56048598E28D");

            entity.Property(e => e.IdCrucero).HasColumnName("idCrucero");
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
            entity.HasKey(e => e.IdDatosPago).HasName("PK__DatosPag__51596242494C45D2");

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
            entity.HasKey(e => e.IdDestino).HasName("PK__Destino__87E69F084F8021A3");

            entity.Property(e => e.IdDestino)
                .ValueGeneratedNever()
                .HasColumnName("idDestino");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetalleReservacion>(entity =>
        {
            entity.HasKey(e => e.IdDetalleRes).HasName("PK__DetalleR__F2234F28D421AAA0");

            entity.Property(e => e.IdDetalleRes)
                .ValueGeneratedNever()
                .HasColumnName("idDetalleRes");
            entity.Property(e => e.IdHabitacion).HasColumnName("idHabitacion");
            entity.Property(e => e.IdReservacion).HasColumnName("idReservacion");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.DetalleReservacion)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleRe__idHab__4CA06362");

            entity.HasOne(d => d.IdReservacionNavigation).WithMany(p => p.DetalleReservacion)
                .HasForeignKey(d => d.IdReservacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleRe__idRes__4D94879B");
        });

        modelBuilder.Entity<Fecha>(entity =>
        {
            entity.HasKey(e => e.IdFecha).HasName("PK__Fecha__65F2E869975D111C");

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
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__D9D53BE2D40A71D3");

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
            entity.HasKey(e => e.IdHuesped).HasName("PK__Huesped__4B73CF97F4FBB45F");

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
                .HasConstraintName("FK__Huesped__idReser__49C3F6B7");
        });

        modelBuilder.Entity<Itinerario>(entity =>
        {
            entity.HasKey(e => e.IdItinerario).HasName("PK__Itinerar__B201E2D656CA249B");

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
            entity.HasKey(e => e.IdPrecio).HasName("PK__Precio__BF8B120C96F4A932");

            entity.Property(e => e.IdPrecio)
                .ValueGeneratedNever()
                .HasColumnName("idPrecio");
            entity.Property(e => e.IdFecha).HasColumnName("idFecha");
            entity.Property(e => e.IdHabitacion).HasColumnName("idHabitacion");
            entity.Property(e => e.PrecioHabitacion).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdFechaNavigation).WithMany(p => p.Precio)
                .HasForeignKey(d => d.IdFecha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Precio__idFecha__59063A47");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Precio)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Precio__idHabita__59FA5E80");
        });

        modelBuilder.Entity<Puerto>(entity =>
        {
            entity.HasKey(e => e.IdPuerto).HasName("PK__Puerto__ADB489103052EAA4");

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
            entity.HasKey(e => e.IdResCom).HasName("PK__ReservaC__7933A5B96D497054");

            entity.Property(e => e.IdResCom)
                .ValueGeneratedNever()
                .HasColumnName("idResCom");
            entity.Property(e => e.IdComplemento).HasColumnName("idComplemento");
            entity.Property(e => e.IdReservacion).HasColumnName("idReservacion");

            entity.HasOne(d => d.IdComplementoNavigation).WithMany(p => p.ReservaComplemento)
                .HasForeignKey(d => d.IdComplemento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReservaCo__idCom__534D60F1");

            entity.HasOne(d => d.IdReservacionNavigation).WithMany(p => p.ReservaComplemento)
                .HasForeignKey(d => d.IdReservacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReservaCo__idRes__52593CB8");
        });

        modelBuilder.Entity<Reservacion>(entity =>
        {
            entity.HasKey(e => e.IdReservacion).HasName("PK__Reservac__C813D8AD28D68921");

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
                .HasConstraintName("FK__Reservaci__idCru__46E78A0C");

            entity.HasOne(d => d.IdDatosPagoNavigation).WithMany(p => p.Reservacion)
                .HasForeignKey(d => d.IdDatosPago)
                .OnDelete(DeleteBehavior.ClientSetNull)

                .HasConstraintName("FK__Reservaci__idDat__44FF419A");
            entity.HasOne(d => d.IdFechaNavigation).WithMany(p => p.Reservacion)
                .HasForeignKey(d => d.IdFecha)
                .OnDelete(DeleteBehavior.ClientSetNull)

                .HasConstraintName("FK__Reservaci__idFec__44FF419A");
            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservacion)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__idUsu__45F365D3");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F76F712BE92");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("idRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__TipoPago__AC5BA85B9B21B522");

            entity.Property(e => e.IdTipoPago)
                .ValueGeneratedNever()
                .HasColumnName("idTipoPago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TransaccionPago>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PK__Transacc__5B8761F041504CB5");

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
                .HasConstraintName("FK__Transacci__idRes__5629CD9C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A61E1E6639");

            entity.HasIndex(e => e.Correo, "UQ__Usuario__60695A1975C251A0").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
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
