using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class Fecha
{
    public int IdFecha { get; set; }

    public DateTime FechaSalida { get; set; }

    public DateTime FechaRegreso { get; set; }

    public virtual ICollection<Precio> Precio { get; set; } = new List<Precio>();

    public virtual ICollection<Crucero> IdCrucero { get; set; } = new List<Crucero>();
}
