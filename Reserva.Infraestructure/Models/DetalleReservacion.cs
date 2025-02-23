using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class DetalleReservacion
{
    public int IdDetalleRes { get; set; }

    public int CantHuespedes { get; set; }

    public int IdHabitacion { get; set; }

    public int IdReservacion { get; set; }

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;

    public virtual Reservacion IdReservacionNavigation { get; set; } = null!;
}
