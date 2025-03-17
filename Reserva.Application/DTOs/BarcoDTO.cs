using Reserva.Infraestructure.Models;

namespace Reserva.Application.DTOs
{
    public record BarcoDTO
    {
        public int IdBarco { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public List<BarcoHabitacionDTO> Habitaciones { get; set; } = new List<BarcoHabitacionDTO>();
        public int CapacidadTotalHuespedes { get; set; }
    }
    // DTO para mostrar habitaciones del barco
    public record BarcoHabitacionDTO
        {
            public int idBarco { get; set; }
            public virtual Barco ? BarcoNavigation { get; set; }
            public int idHabitacion { get; set; }
            public virtual Habitacion ? HabitacionNavigation { get; set; }
            public int CantHabitaciones { get; set; }
            public string ? NombreHabitacion { get; set; } = null!;
            public int ? HuespedesMax { get; set; }
    }
}
