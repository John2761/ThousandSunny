using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public class ReservacionFormDTO
    {
        public List<FechaDTO> Fechas { get; set; } = new();
        public List<ComplementoDTO> Complementos { get; set; } = new();
    }

    public class FechaDTO
    {
        public int IdFecha { get; set; }
        public DateTime FechaSalida { get; set; }
        public int IdCrucero { get; set; }
        public string NombreCrucero { get; set; } = null!;
    }

    public class ComplementoDTO
    {
        public int IdComplemento { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Tipo { get; set; } // Habitacion o Pasajero
        public decimal Precio { get; set; }
    }

}
