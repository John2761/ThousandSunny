namespace Reserva.Application.DTOs
{
    public class BarcoDTO
    {
        public int IdBarco { get; set; } = 0!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        // Lista de habitaciones y su cantidad en el barco
        public List<HabitacionBarcoDTO> Habitaciones { get; set; } = new();
        // Capacidad total de huéspedes del barco
        public int CapacidadTotalHuespedes { get; set; }
    }
    // DTO para mostrar habitaciones del barco
    public class HabitacionBarcoDTO
        {
            public string NombreHabitacion { get; set; } = null!;
            public int CantidadHabitaciones { get; set; }
            public int HuespedesMax { get; set; }
    }

}
