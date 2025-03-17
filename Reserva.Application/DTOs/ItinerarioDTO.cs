using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record ItinerarioDTO
    {
        [Required(ErrorMessage = "El día del itinerario es obligatorio")]
        [Range(1, 365, ErrorMessage = "El día del itinerario debe estar entre 1 y 365")]
        public int Dia { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripción para el itinerario")]
        
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un puerto")]
        public int IdPuerto { get; set; } = new();
        public string? NombrePuerto { get; set; } // Se usa en la vista de detalle

    }
}
