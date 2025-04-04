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
                    .Include(r => r.IdCruceroNavigation)
                    .ThenInclude(c => c.Itinerario)
                    .ThenInclude(i => i.IdPuertoNavigation)
                .Include(r => r.IdFechaNavigation)
                .Include(r => r.IdDatosPagoNavigation)
                .Include(r => r.DetalleReservacion)
                    .ThenInclude(d => d.IdHabitacionNavigation)
                    .ThenInclude(h => h.Precio)
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
        public async Task<List<Fecha>> ListFechasAsync()
        {
            return await _context.Fecha.Include(f => f.IdCruceroNavigation).ToListAsync();
        }

        public async Task<List<Complemento>> ListComplementosAsync()
        {
            return await _context.Complemento.ToListAsync();
        }
        public async Task<List<Itinerario>> GetItinerariosPorIdCruceroAsync(int idCrucero)
        {
            return await _context.Itinerario
                .Where(i => i.IdCrucero == idCrucero)
                .ToListAsync();
        }

        public async Task<List<(Habitacion habitacion, decimal precio)>> GetHabitacionesConPrecioPorFechaAsync(int idFecha)
        {
            var fecha = await _context.Fecha
                .Include(f => f.IdCruceroNavigation)
                .FirstOrDefaultAsync(f => f.IdFecha == idFecha);

            if (fecha == null) return new();

            var idBarco = fecha.IdCruceroNavigation.IdBarco;

            var habitaciones = await _context.BarcoHabitacion
                .Include(bh => bh.IdHabitacionNavigation)
                .Where(bh => bh.IdBarco == idBarco)
                .ToListAsync();

            var precios = await _context.Precio
                .Where(p => p.IdFecha == idFecha)
                .ToListAsync();

            var resultado = habitaciones.Select(h =>
            {
                var habitacion = h.IdHabitacionNavigation;
                var precio = precios.FirstOrDefault(p => p.IdHabitacion == h.IdHabitacion)?.PrecioHabitacion ?? 0;
                return (habitacion, precio);
            }).ToList();

            return resultado;
        }


    }
}
