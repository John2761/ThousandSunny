using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record  FechaPrecioDTO
    {
        [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
        public DateTime FechaSalida { get; set; }

        [MinLength(1, ErrorMessage = "Debe haber al menos una habitación con precio registrado.")]
        public List<PrecioHabitacionDTO> PrecioHabitacion { get; set; } = new();
    }
}
