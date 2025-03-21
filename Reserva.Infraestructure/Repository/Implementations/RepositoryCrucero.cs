using Microsoft.EntityFrameworkCore;
using Reserva.Infraestructure.Data;
using Reserva.Infraestructure.Models;
using Reserva.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Infraestructure.Repository.Implementations
{
    public class RepositoryCrucero : IRepositoryCrucero
    {
        private readonly ThousandSunnyContext _context;

        public RepositoryCrucero(ThousandSunnyContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Crucero crucero)
        {
            await _context.Set<Crucero>().AddAsync(crucero);
            await _context.SaveChangesAsync();
            return crucero.IdCrucero;
        }

        public async Task<List<Habitacion>> ObtenerHabitacionesPorBarcoAsync(int idBarco)
        {
            return await _context.Habitacion
                .Where(h => h.BarcoHabitacion.Any(bh => bh.IdBarco == idBarco))
        .Include(h => h.Precio) // ✅ Aplicado antes de Select()
        .ToListAsync();
        }

        public async Task<Crucero?> FindByIdAsync(int id)
        {
            return await _context.Set<Crucero>()
                .Include(c => c.IdBarcoNavigation)
                .Include(c => c.Itinerario)
                    .ThenInclude(i => i.IdPuertoNavigation)
                .Include(c => c.Fecha)
                    .ThenInclude(f => f.Precio)
                        .ThenInclude(p => p.IdHabitacionNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdCrucero == id);
        }

        public async Task<ICollection<Crucero>> ListAsync()
        {
            return await _context.Set<Crucero>()
                .Include(c => c.IdBarcoNavigation)
                .Include(c => c.Itinerario)
                    .ThenInclude(i => i.IdPuertoNavigation)
                .Include(c => c.Fecha)
                    .ThenInclude(f => f.Precio)
                        .ThenInclude(p => p.IdHabitacionNavigation)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
