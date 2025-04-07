using System.ComponentModel.DataAnnotations;

namespace Reserva.Web.ViewModels
{
    public class PagoViewModel
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string CorreoUsuario { get; set; } = string.Empty;

        public decimal TotalReserva { get; set; }
        public decimal MontoDeposito { get; set; }
        public decimal MontoRestante => TotalReserva - MontoDeposito;

        public DateTime FechaLimitePago { get; set; }

        public string TipoPago { get; set; } = "Total"; // "Total" o "Deposito"

        [Required, CreditCard]
        public string NumeroTarjeta { get; set; } = string.Empty;

        [Required]
        public string FechaExpiracion { get; set; } = string.Empty;

        [Required, StringLength(4, MinimumLength = 3)]
        public string CVV { get; set; } = string.Empty;

        [Required]
        public string NombreTitular { get; set; } = string.Empty;
    }
}
