using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record CruceroDTO
    {
        public int IdCrucero { get; set; }

        [Required(ErrorMessage = "El nombre del crucero es obligatorio")]
        
        public string Nombre { get; set; } = null!;

       
        public string? Descripcion { get; set; } 

        [Range(1, 365, ErrorMessage = "La duración debe estar entre 1 y 365 días")]
        public int CantidadDias { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un barco")]
        public string NombreBarco { get; set; } = null!;

        [MinLength(2, ErrorMessage = "Debe agregar al menos 2 puertos al itinerario")]
        public List<ItinerarioDTO> Itinerario { get; set; } = new List<ItinerarioDTO>();
        public List<FechaPrecioDTO> FechasPrecios { get; set; } = new List<FechaPrecioDTO>();
    }
}
