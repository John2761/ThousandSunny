using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.DTOs
{
    public record DashboardDTO
    {
        public string NombreCrucero { get; set; }
        public DateTime FechaSalida { get; set; }
        public int CantidadReservas { get; set; }
        public Dictionary<string, int> HabitacionesDisponiblesPorTipo { get; set; }
        public int CantidadHuespedes { get; set; }
    }
}
