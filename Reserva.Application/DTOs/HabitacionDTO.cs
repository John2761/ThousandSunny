using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record HabitacionDTO
    {
        public int IdHabitacion { get; set; }

        [Required(ErrorMessage = "El nombre de la habitación es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "Debe seleccionar un tamaño.")]
        [RegularExpression("Pequeña|Mediana|Grande|Extra Grande", ErrorMessage = "El tamaño debe ser Pequeña, Mediana, Grande o Extra Grande.")]
        public string Tamaño { get; set; } = null!;

        [Required(ErrorMessage = "El número mínimo de huéspedes es obligatorio.")]
        [Range(1, 10, ErrorMessage = "El mínimo de huéspedes debe estar entre 1 y 10.")]
        public int HuespedesMin { get; set; }

        [Required(ErrorMessage = "El número máximo de huéspedes es obligatorio.")]
        [Range(1, 10, ErrorMessage = "El máximo de huéspedes debe estar entre 1 y 10.")]
        public int HuespedesMax { get; set; }
        public int? Cantidad { get; set; }
    }
}
