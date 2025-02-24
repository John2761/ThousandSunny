using Reserva.Infraestructure.Models;

namespace Reserva.Application.DTOs
{
    public class ReservacionDTO
    {
        public int idReservacion { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaLimitePago { get; set; }

        public string NombreCrucero { get; set; } = null!;
        public string NombreBarco { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin => FechaInicio.AddDays(CantidadDias);
        public string EstadoPago { get; set; } = null!;
        public int CantidadDias { get; set; }
        public string PuertoSalida { get; set; } = null!;
        public string PuertoRegreso { get; set; } = null!;
        public List<HabitacionesReservaDTO> Habitaciones { get; set; } = new();
        public List<ComplementoReservaDTO> Complementos { get; set; } = new();
        public List<HuespedDTO> Huespedes { get; set; } = new();
        public decimal TotalHabitaciones { get; set; }
        public decimal TotalComplementos { get; set; }
        public decimal SubTotal => TotalHabitaciones + TotalComplementos;
        public decimal Impuestos => SubTotal * 0.13m; // 13% de IVA
        public decimal TotalPagar => SubTotal + Impuestos;
        public decimal? MontoPendiente { get; set; } // Solo si el pago está pendiente


    }
}
