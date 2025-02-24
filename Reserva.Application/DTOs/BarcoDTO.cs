using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public class BarcoDTO
    {
        public int IdBarco { get; set; } = 0!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

    }
}
