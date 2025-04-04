using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record HabitacionesReservaDTO
    {
        public string NombreHabitacion { get; set; } = null!;
        public int CantidadHuespedes { get; set; }
        public decimal PrecioTotal { get; set; }

    }
}