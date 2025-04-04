using Microsoft.AspNetCore.Mvc.Rendering;
using Reserva.Application.DTOs;

namespace Reserva.Web.ViewModels
{
    public class ReservacionViewModel
    {
        public int IdFecha { get; set; }
        public List<SelectListItem> FechasDisponibles { get; set; } = new();
        public List<FechaDTO> Fechas { get; set; } = new();

        public int IdItinerario { get; set; }
        public List<SelectListItem> Itinerarios { get; set; } = new();

        public List<HabitacionSeleccionadaViewModel> Habitaciones { get; set; } = new();
        public List<HuespedInputModel> Huespedes { get; set; } = new();

        public List<ComplementoSeleccionadoViewModel> Complementos { get; set; } = new();

        public DateTime FechaReserva => DateTime.Now;
        public DateTime? FechaLimitePago { get; set; }
    }

    public class HabitacionSeleccionadaViewModel
    {
        public int IdHabitacion { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public bool Seleccionada { get; set; }
    }

    public class HuespedInputModel
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public int Edad { get; set; }
    }


    public class ComplementoSeleccionadoViewModel
    {
        public int IdComplemento { get; set; }
        public string Nombre { get; set; }
        public bool Tipo { get; set; } // “Habitacion” o “Pasajero”
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }

}
