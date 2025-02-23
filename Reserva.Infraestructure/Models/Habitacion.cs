using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class Habitacion
{
    public int IdHabitacion { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Tamaño { get; set; }

    public int HuespedesMin { get; set; }

    public int HuespedesMax { get; set; }

    public virtual ICollection<BarcoHabitacion> BarcoHabitacion { get; set; } = new List<BarcoHabitacion>();

    public virtual ICollection<DetalleReservacion> DetalleReservacion { get; set; } = new List<DetalleReservacion>();

    public virtual ICollection<Precio> Precio { get; set; } = new List<Precio>();
}
