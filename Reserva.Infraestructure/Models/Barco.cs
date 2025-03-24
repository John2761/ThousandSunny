using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class Barco
{
    public int IdBarco { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<BarcoHabitacion> BarcoHabitacion { get; set; } = new List<BarcoHabitacion>();

    public virtual ICollection<Crucero> Crucero { get; set; } = new List<Crucero>();
}
