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
                        .ThenInclude(bh => bh.HabitacionNavigation)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(b => b.IdBarco == id);
        }

        public async Task<ICollection<Barco>> ListAsync()
        {
            return await _context.Barco.ToListAsync();
        }
        public async Task<int> AddAsync(BarcoHabitacion entity)
        {
            try
            {
                // Begin Transaction
                await _context.Database.BeginTransactionAsync();
                await _context.Set<BarcoHabitacion>().AddAsync(entity);
                // Actualizar inventario
                foreach (var item in entity.BarcoNavigation.BarcoHabitacion)
                {
                    //Buscar Habitacion
                    var Habitacion = await _context.Set<Habitacion>().FindAsync(item.IdHabitacion);
                    //Actualizar Habitacion
                    _context.Set<Habitacion>().Update(entity.HabitacionNavigation);
                }
                await _context.SaveChangesAsync();
                // Commit
                await _context.Database.CommitTransactionAsync();

                return entity.IdBarco;
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                // Rollback 
                await _context.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddAsyncHabitacion(BarcoHabitacion entity, int idBarco)
        {
            await _context.Set<BarcoHabitacion>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.IdBarco;
        }

        public Task<bool> UpdateAsync(Barco barco)
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
