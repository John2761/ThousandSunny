using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class ReservaComplemento
{
    public int IdResCom { get; set; }

    public int Cantidad { get; set; }

    public int IdReservacion { get; set; }

    public int IdComplemento { get; set; }

    public virtual Complemento IdComplementoNavigation { get; set; } = null!;

    public virtual Reservacion IdReservacionNavigation { get; set; } = null!;
}
