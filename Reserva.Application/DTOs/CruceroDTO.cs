using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record CruceroDTO
    {
        public int IdCrucero { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int CantidadDias { get; set; }
        public string NombreBarco { get; set; } = null!;
        public List<ItinerarioDTO> Itinerario { get; set; } = new();
        public List<FechaPrecioDTO> FechasPrecios { get; set; } = new();
    }
}
