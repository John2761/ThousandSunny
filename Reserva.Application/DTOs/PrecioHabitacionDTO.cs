using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record PrecioHabitacionDTO
    {
        public string NombreHabitacion { get; set; } = null;
        public decimal PrecioHabitacion {  set; get; }
    }
}
