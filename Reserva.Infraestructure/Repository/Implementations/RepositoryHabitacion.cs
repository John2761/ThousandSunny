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

        public async Task<int> AddAsync(Habitacion habitacion)
        {
            
            await _context.Habitacion.AddAsync(habitacion);
            await _context.SaveChangesAsync();
            return habitacion.IdHabitacion;
        }

        public async Task<Habitacion> FindByIdAsync(int id)
        {
            return await _context.Habitacion.FindAsync(id);
        }

        public async Task<ICollection<Habitacion>> ListAsync()
        {
            return await _context.Habitacion.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Habitacion habitacion)
        {
            _context.Habitacion.Update(habitacion);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _context.Habitacion.AnyAsync(h => h.Nombre == nombre);
        }
    }
}
