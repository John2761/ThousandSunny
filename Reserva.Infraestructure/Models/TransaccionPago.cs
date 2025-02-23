using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class TransaccionPago
{
    public int IdTransaccion { get; set; }

    public int Estado { get; set; }

    public string? CodigoAutorizacion { get; set; }

    public DateOnly FechaTransaccion { get; set; }

    public int IdReservacion { get; set; }

    public virtual Reservacion IdReservacionNavigation { get; set; } = null!;
}
