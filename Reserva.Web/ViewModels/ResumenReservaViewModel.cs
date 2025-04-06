using Microsoft.AspNetCore.Mvc.Rendering;

namespace Reserva.Web.ViewModels
{

    public class ResumenReservaViewModel
    {
        public string NombreCrucero { get; set; }
        public string PuertoSalida { get; set; }
        public string PuertoRegreso { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public List<HabitacionResumenViewModel> Habitaciones { get; set; } = new();
        public List<ComplementoResumenViewModel> Complementos { get; set; } = new();

        public decimal TotalHabitaciones => Habitaciones.Sum(h => h.Precio);
        public decimal TotalComplementos => Complementos.Sum(c => c.Total);
        public decimal Subtotal => TotalHabitaciones + TotalComplementos;
        public decimal Impuestos => Subtotal * 0.13m;
        public decimal Total => Subtotal + Impuestos;
    }


    public class HabitacionResumenViewModel
    {
        public string Nombre { get; set; } = "";
        public int CantidadHuespedes { get; set; }
        public decimal Precio { get; set; }
    }

    public class ComplementoResumenViewModel
    {
        public string Nombre { get; set; } = "";
        public string Tipo { get; set; } = ""; // Ej: habitación / pasajero
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total => Cantidad * PrecioUnitario;
    }
}