using System;
using System.Collections.Generic;

namespace Reserva.Infraestructure.Models;

public partial class Complemento
{
    public int IdComplemento { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public bool TipoAplicacion { get; set; }

    public virtual ICollection<ReservaComplemento> ReservaComplemento { get; set; } = new List<ReservaComplemento>();
}
