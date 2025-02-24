using Microsoft.EntityFrameworkCore;
using Reserva.Infraestructure.Data;
using Reserva.Infraestructure.Models;

namespace Reserva.Infraestructure.Repository.Implementations
{
    public class RepositoryReservacion
    {
        private readonly ThousandSunnyContext _context;

        public RepositoryReservacion(ThousandSunnyContext context)
        {
            _context = context;
        }

        public async Task<Reservacion> FindByIdAsync(int id)
        {
            return await _context.Reservacion.FindAsync(id);
        }

        public async Task<ICollection<Reservacion>> ListAsync()
        {
            return await _context.Reservacion.ToListAsync();
        }
    }
}
