using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class Fecha
{
    public int IdFecha { get; set; }

    public DateTime FechaSalida { get; set; }

    public int IdCrucero { get; set; }

    public virtual Crucero IdCruceroNavigation { get; set; } = null!;

    public virtual ICollection<Precio> Precio { get; set; } = new List<Precio>();

    public virtual ICollection<Reservacion> Reservacion { get; set; } = new List<Reservacion>();
}
