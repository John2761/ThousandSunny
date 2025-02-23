using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class Huesped
{
    public int IdHuesped { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string? Apellido2 { get; set; }

    public string? Telefono { get; set; }

    public int IdReservacion { get; set; }

    public virtual Reservacion IdReservacionNavigation { get; set; } = null!;
}
