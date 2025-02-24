using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record HabitacionDTO
    {
        public int IdHabitacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Tamaño { get; set; } = null!;
        public int HuespedesMin { get; set; }
        public int HuespedesMax { get; set; }
    }
}
