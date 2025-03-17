using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reserva.Infraestructure.Models;

public partial class Barco
{
    [Key]
    public int IdBarco { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<BarcoHabitacion> BarcoHabitacion { get; set; } = new List<BarcoHabitacion>();

    public virtual ICollection<Crucero> Crucero { get; set; } = new List<Crucero>();
}
