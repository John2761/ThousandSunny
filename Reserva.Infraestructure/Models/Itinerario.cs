using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class Itinerario
{
    public int IdItinerario { get; set; }

    public int Dia { get; set; }

    public string? Descripcion { get; set; }

    public int IdPuerto { get; set; }

    public int IdCrucero { get; set; }

    public virtual Crucero IdCruceroNavigation { get; set; } = null!;

    public virtual Puerto IdPuertoNavigation { get; set; } = null!;
}
