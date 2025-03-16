using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;

namespace Reserva.Application.Services.Interfaces
{
    public interface IServiceHabitacion
    {
        Task<ICollection<HabitacionDTO>> ListAsync();
        Task<HabitacionDTO> FindByIdAsync(int id);
        Task<int> AddAsync(HabitacionDTO habitacionDto);
        Task<bool> UpdateAsync(HabitacionDTO habitacionDto);
    }
}
