using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record  FechaPrecioDTO
    {
        public DateTime FechaSalida { get; set; }
        public List<PrecioHabitacionDTO> PrecioHabitacion { get; set; } = new();
    }
}
