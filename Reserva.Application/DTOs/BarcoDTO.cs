namespace Reserva.Application.DTOs
{
    public record BarcoDTO
    {
        public int IdBarco { get; set; } 
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        // Lista de habitaciones y su cantidad en el barco
        public List<HabitacionBarcoDTO> Habitaciones { get; set; } = new List<HabitacionBarcoDTO>();
        // Capacidad total de huéspedes del barco
        public int CapacidadTotalHuespedes { get; set; }
    }
    // DTO para mostrar habitaciones del barco
    public record HabitacionBarcoDTO
        {
            public string NombreHabitacion { get; set; } = null!;
            public int CantidadHabitaciones { get; set; }
            public int HuespedesMax { get; set; }
    }

}
