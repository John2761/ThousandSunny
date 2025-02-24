namespace Reserva.Infraestructure.Models;

public partial class Reservacion
{
    public int IdReservacion { get; set; }

    public DateTime FechaLimite { get; set; }

    public DateTime FechaReserva { get; set; }

    public int IdFecha { get; set; }

    public int IdUsuario { get; set; }

    public int IdCrucero { get; set; }

    public int IdDatosPago { get; set; }

    public virtual ICollection<DetalleReservacion> DetalleReservacion { get; set; } = new List<DetalleReservacion>();

    public virtual ICollection<Huesped> Huesped { get; set; } = new List<Huesped>();

    public virtual Crucero IdCruceroNavigation { get; set; } = null!;

    public virtual DatosPago IdDatosPagoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    public virtual Fecha IdFechaNavigation { get; set; } = null!;

    public virtual ICollection<ReservaComplemento> ReservaComplemento { get; set; } = new List<ReservaComplemento>();

    public virtual ICollection<TransaccionPago> TransaccionPago { get; set; } = new List<TransaccionPago>();
}
