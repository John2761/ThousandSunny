namespace Reserva.Application.DTOs
{
    public class ReservacionDTO
    {
        public int idReservacion { get; set; } = 0!;
        public DateTime FechaReserva { get; set; } = DateTime.Now; // Fecha en la que se hizo la reserva
        public DateTime FechaLimite { get; set; } = DateTime.Now; //Ultima fecha para pagar la reserva
        public int idFecha { get; set; } = 0!; // Fechas del crucero
        public int idCrucero { get; set; } = 0!; // Crucero reservado
        public int idDatosPago { get; set; } = 0!; // Datos con los que se paga

    }
}
