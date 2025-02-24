using Microsoft.EntityFrameworkCore;
using Reserva.Infraestructure.Data;
using Reserva.Infraestructure.Models;
using Reserva.Infraestructure.Repository.Interfaces;

namespace Reserva.Infraestructure.Repository.Implementations
{
    public class RepositoryBarco : IRepositoryBarco
    {
        private readonly ThousandSunnyContext _context;

        public RepositoryBarco(ThousandSunnyContext context)
        {
            _context = context;
        }

        public async Task<Barco> FindByIdAsync(int id)
        {
            return await _context.Barco
                        .Include(b => b.BarcoHabitacion)
                        .ThenInclude(bh => bh.IdHabitacionNavigation)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(b => b.IdBarco == id);
        }

        public async Task<ICollection<Barco>> ListAsync()
        {
            return await _context.Barco.ToListAsync();
        }
    }
}
