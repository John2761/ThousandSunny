using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record HuespedDTO
    {
        public string Nombre { get; set; } = null!;
        public string Apellido1{ get; set; } = null!;
        public string? Apellido2 { get; set; } 
        public string Telefono { get; set; } = null!;
    }
}
