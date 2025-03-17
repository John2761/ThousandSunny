using Microsoft.EntityFrameworkCore;
using Reserva.Infraestructure.Models;

namespace Reserva.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryBarco
    {
        Task<ICollection<Barco>> ListAsync();

        Task<Barco> FindByIdAsync(int Id);

        Task<int> AddAsync(BarcoHabitacion BH);

        Task<bool> UpdateAsync(Barco barco);

        Task<int> GetNextNumberBarco();

    }
}