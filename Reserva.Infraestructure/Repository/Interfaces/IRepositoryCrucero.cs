using Reserva.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryCrucero
    {
        Task<ICollection<Crucero>> ListAsync();
        Task<Crucero> FindByIdAsync(int id);
    }
}
