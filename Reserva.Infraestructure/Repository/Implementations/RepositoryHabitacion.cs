using Microsoft.EntityFrameworkCore;
using Reserva.Infraestructure.Data;
using Reserva.Infraestructure.Models;
using Reserva.Infraestructure.Repository.Interfaces;

namespace Reserva.Infraestructure.Repository.Implementations
{
    public class RepositoryHabitacion : IRepositoryHabitacion
    {
        private readonly ThousandSunnyContext _context;

        public RepositoryHabitacion(ThousandSunnyContext context)
        {
            _context = context;
        }

        public async Task<Habitacion> FindByIdAsync(int id)
        {
            return await _context.Habitacion.FindAsync(id);
        }

        public async Task<ICollection<Habitacion>> ListAsync()
        {
            return await _context.Habitacion.ToListAsync();
        }
    }
}
