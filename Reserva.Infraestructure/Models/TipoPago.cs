using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class TipoPago
{
    public int IdTipoPago { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<DatosPago> DatosPago { get; set; } = new List<DatosPago>();
}
