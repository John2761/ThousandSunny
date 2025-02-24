using Reserva.Infraestructure.Models;

namespace Reserva.Application.DTOs
{
    public class ReservacionDTO
    {
        public int idReservacion { get; set; } = 0!;

        public DateTime FechaReserva { get; set; } = DateTime.Now!; // Fecha en la que se hizo la reserva

        public DateTime FechaLimitePago { get; set; } = DateTime.Now!;  //Ultima fecha para pagar la reserva

        public int idFecha { get; set; } = 0!; // Fechas del crucero

        public int IdUsuario { get; set; } // id del usuario que reserva

        public int idCrucero { get; set; } = 0!; // Crucero reservado
        
        public int idDatosPago { get; set; } = 0!; // Datos con los que se paga

        public virtual ICollection<DetalleReservacion> DetalleReservacion { get; set; } = new List<DetalleReservacion>();

        public virtual ICollection<Huesped> Huesped { get; set; } = new List<Huesped>();

        public virtual Crucero IdCruceroNavigation { get; set; } = null!;

        public virtual DatosPago IdDatosPagoNavigation { get; set; } = null!;

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

        public virtual ICollection<ReservaComplemento> ReservaComplemento { get; set; } = new List<ReservaComplemento>();

        public virtual ICollection<TransaccionPago> TransaccionPago { get; set; } = new List<TransaccionPago>();
    }
}
