using System.ComponentModel.DataAnnotations;

namespace Reserva.Web.ViewModels
{
    public class ViewModelInput
    {
        [Display(Name = "Habitación")]
        public int IdHabitacion { get; set; } // IdHabitacion

        [Display(Name = "Capacidad")]
        [Range(1, 999, ErrorMessage = "La capacidad mínima es {1}")]
        public int Capacidad { get; set; } //Cantidad a Capacidad

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } //Descripción de la habitación

        [Display(Name = "Imagen")]
        public string Imagen { get; set; }
    }
}