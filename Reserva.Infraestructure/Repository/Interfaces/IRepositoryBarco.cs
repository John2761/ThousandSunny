using Microsoft.EntityFrameworkCore;
using Reserva.Infraestructure.Models;

namespace Reserva.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryBarco
    {
        Task<ICollection<Barco>> ListAsync();
        Task<Barco> FindByIdAsync(int Id);
        Task<Barco> AddAsync(Barco barco);
        Task<Barco> UpdateAsync(int id, Barco barco);
        Task<int> GetNextNumberBarco();

    }
}