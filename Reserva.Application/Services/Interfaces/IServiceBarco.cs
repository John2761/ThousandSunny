using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;

namespace Reserva.Application.Services.Interfaces
{
    public interface IServiceBarco
    {
        Task<ICollection<BarcoDTO>> ListAsync();
        Task<BarcoDTO> FindByIdAsync(int id);
        Task<Barco> AddAsync(BarcoDTO Barco);
        Task UpdateAsync(int id, BarcoDTO barco);
        Task<int> GetNextNumberBarco();
    }
}
