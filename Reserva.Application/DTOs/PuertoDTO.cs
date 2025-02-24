using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record PuertoDTO
    {
        public int IdPuerto { get; set; }
        public string Nombre { get; set; } = null!;
        
    }
}
