using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record ItinerarioDTO
    {
        public int Dia { get; set; }
        public string Descripcion { get; set; } = null;
        public PuertoDTO Puerto { get; set; } = new();

    }
}
