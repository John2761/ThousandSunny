using Microsoft.EntityFrameworkCore;
using Reserva.Infraestructure.Data;
using Reserva.Infraestructure.Models;
using Reserva.Infraestructure.Repository.Interfaces;

namespace Reserva.Infraestructure.Repository.Implementations
{
    public class RepositoryReservacion : IRepositoryReservacion
    {
        private readonly ThousandSunnyContext _context;

        public RepositoryReservacion(ThousandSunnyContext context)
        {
            _context = context;
        }

        public async Task<Reservacion?> FindByIdAsync(int id)
        {
            return await _context.Reservacion
                .Include(r => r.IdCruceroNavigation)
                    .ThenInclude(c => c.IdBarcoNavigation)
                .Include(r => r.IdFechaNavigation)
                .Include(r => r.DetalleReservacion)
                    .ThenInclude(d => d.IdHabitacionNavigation)
                .Include(r => r.ReservaComplemento)
                    .ThenInclude(c => c.IdComplementoNavigation)
                .Include(r => r.Huesped)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.IdReservacion == id);
        }

        public async Task<ICollection<Reservacion>> ListAsync()
        {
            return await _context.Set<Reservacion>()
        .Include(r => r.IdCruceroNavigation)
        .Include(r => r.IdFechaNavigation)
        .Include(r => r.IdDatosPagoNavigation)
        .Include(r => r.DetalleReservacion)
            .ThenInclude(d => d.IdHabitacionNavigation)
        .Include(r => r.ReservaComplemento)
            .ThenInclude(rc => rc.IdComplementoNavigation)
        .AsNoTracking()
        .ToListAsync();
        }
    }
}
