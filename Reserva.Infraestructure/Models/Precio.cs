using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class Precio
{
    public int IdPrecio { get; set; }

    public decimal PrecioHabitacion { get; set; }

    public int IdFecha { get; set; }

    public int IdHabitacion { get; set; }

    public virtual Fecha IdFechaNavigation { get; set; } = null!;

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;
}
