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
    public class RepositoryPuerto : IRepositoryPuerto
    {
        private readonly ThousandSunnyContext _context;

        public RepositoryPuerto(ThousandSunnyContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Puerto>> ListAsync()
        {
            return await _context.Puerto.AsNoTracking().ToListAsync();
        }

        public async Task<Puerto?> FindByIdAsync(int id)
        {
            return await _context.Puerto.FirstOrDefaultAsync(p => p.IdPuerto == id);
        }
    }
}
