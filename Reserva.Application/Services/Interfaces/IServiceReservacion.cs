using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;

namespace Reserva.Application.Services.Interfaces
{
    public interface IServiceReservacion
    {
        Task<ICollection<ReservacionDTO>> ListAsync();
        Task<ReservacionDTO?> FindByIdAsync(int id);
        Task<ReservacionFormDTO> GetReservaFormDataAsync();
        Task<List<ItinerarioInfoDTO>> GetItinerariosPorCruceroAsync(int idCrucero, DateTime? fechaSalida);
        Task<List<HabitacionDTO>> GetHabitacionesPorFechaAsync(int idfecha);
    }
}