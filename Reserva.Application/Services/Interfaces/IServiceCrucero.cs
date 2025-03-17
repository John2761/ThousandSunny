using Reserva.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.Services.Interfaces
{
    public interface IServiceCrucero
    {
        Task<ICollection<CruceroDTO>> ListAsync();
        Task<CruceroDTO?> FindByIdAsync(int id);
        Task<int> AddAsync(CruceroDTO cruceroDto);
        Task<List<HabitacionDTO>> ObtenerHabitacionesPorBarcoAsync(int idBarco);
    }
}
