using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record ComplementoReservaDTO
    {
        public string NombreComplemento { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
