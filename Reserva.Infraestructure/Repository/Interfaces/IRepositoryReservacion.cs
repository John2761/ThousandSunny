
using Reserva.Infraestructure.Models;

namespace Reserva.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryReservacion
    {
        Task<ICollection<Reservacion>> ListAsync();
        Task<Reservacion> FindByIdAsync(int id);
    }
}
