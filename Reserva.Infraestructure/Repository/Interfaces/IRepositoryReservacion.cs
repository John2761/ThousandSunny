
using Reserva.Infraestructure.Models;

namespace Reserva.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryReservacion
    {
        Task<ICollection<Reservacion>> ListAsync();
        Task<Reservacion?> FindByIdAsync(int id);
        Task<List<Fecha>> ListFechasAsync();
        Task<List<Complemento>> ListComplementosAsync();
        Task<List<Itinerario>> GetItinerariosPorIdCruceroAsync(int idCrucero);
        Task<List<(Habitacion habitacion, decimal precio)>> GetHabitacionesConPrecioPorFechaAsync(int idFecha);
        Task<List<Precio>> GetPreciosPorFechaAsync(int idFecha);

    }
}
