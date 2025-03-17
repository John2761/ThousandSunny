using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;

namespace Reserva.Application.Services.Interfaces
{
    public interface IServiceBarco
    {
        Task<ICollection<BarcoDTO>> ListAsync();
        Task<BarcoDTO> FindByIdAsync(int id);
        Task<int> AddAsync(BarcoHabitacionDTO bhDTO);
        Task<bool> UpdateAsync(BarcoDTO barco);
        Task<int> GetNextNumberBarco();
    }
}
