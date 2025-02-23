using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class Puerto
{
    public int IdPuerto { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdDestino { get; set; }

    public virtual Destino IdDestinoNavigation { get; set; } = null!;

    public virtual ICollection<Itinerario> Itinerario { get; set; } = new List<Itinerario>();
}
