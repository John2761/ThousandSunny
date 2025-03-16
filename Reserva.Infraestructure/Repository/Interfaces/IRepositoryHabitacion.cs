using Reserva.Infraestructure.Models;

namespace Reserva.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryHabitacion
    {
        Task<ICollection<Habitacion>> ListAsync();
        Task<Habitacion> FindByIdAsync(int Id);
        Task<int> AddAsync(Habitacion habitacion);
        Task<bool> UpdateAsync(Habitacion habitacion);
        Task<bool> ExisteNombreAsync(string nombre);
    }
}
