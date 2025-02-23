using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class Crucero
{
    public int IdCrucero { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int CantidadDias { get; set; }

    public int IdBarco { get; set; }

    public virtual Barco IdBarcoNavigation { get; set; } = null!;

    public virtual ICollection<Itinerario> Itinerario { get; set; } = new List<Itinerario>();

    public virtual ICollection<Reservacion> Reservacion { get; set; } = new List<Reservacion>();

    public virtual ICollection<Fecha> IdFecha { get; set; } = new List<Fecha>();
}
