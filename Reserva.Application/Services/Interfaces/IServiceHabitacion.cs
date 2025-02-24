using Reserva.Application.DTOs;

namespace Reserva.Application.Services.Interfaces
{
    public interface IServiceHabitacion
    {
        Task<ICollection<HabitacionDTO>> ListAsync();
        Task<HabitacionDTO> FindByIdAsync(int id);
    }
}
