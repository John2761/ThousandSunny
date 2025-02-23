using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class BarcoHabitacion
{
    public int IdBarco { get; set; }

    public int IdHabitacion { get; set; }

    public int CantHabitaciones { get; set; }

    public virtual Barco IdBarcoNavigation { get; set; } = null!;

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;
}
