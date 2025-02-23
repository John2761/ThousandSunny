using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class DatosPago
{
    public int IdDatosPago { get; set; }

    public decimal MontoTotal { get; set; }

    public decimal? MontoPrima { get; set; }

    public decimal? MontoPendiente { get; set; }

    public int IdTipoPago { get; set; }

    public virtual TipoPago IdTipoPagoNavigation { get; set; } = null!;

    public virtual ICollection<Reservacion> Reservacion { get; set; } = new List<Reservacion>();
}
