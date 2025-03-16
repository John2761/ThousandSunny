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
        public async Task<Barco> AddAsync(Barco entity)
        {
            await _context.Set<Barco>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<Barco> UpdateAsync(int id, Barco barco)
        {
            throw new NotImplementedException();
        }
        public async Task<int> GetNextNumberBarco()
        {
            int current = 0;
            string sql = "SELECT COALESCE(MAX(idBarco), 0) + 1 FROM Barco;";

            System.Data.DataTable dataTable = new System.Data.DataTable();
            System.Data.Common.DbConnection connection = _context.Database.GetDbConnection();
            System.Data.Common.DbProviderFactory dbFactory = System.Data.Common.DbProviderFactories.GetFactory(connection!)!;

            using (var cmd = dbFactory!.CreateCommand())
            {
                cmd!.Connection = connection;
                cmd.CommandText = sql;

                using (System.Data.Common.DbDataAdapter adapter = dbFactory.CreateDataAdapter()!)
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }
            }

            if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
            {
                if (int.TryParse(dataTable.Rows[0][0].ToString(), out int result))
                {
                    current = result;
                }
            }

            return await Task.FromResult(current);
        }


        public async Task<ICollection<Habitacion>> getHabitaciones(string[] selectedHabitaciones)
        {
            // Buscar o crear categorías
            var Habitaciones = await _context.Set<Habitacion>()
            .Where(h => selectedHabitaciones.Contains(h.IdHabitacion.ToString()))
            .ToListAsync();
            return Habitaciones;
        }
    }
}
