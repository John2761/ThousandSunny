using Reserva.Application.DTOs;

namespace Reserva.Application.Services.Interfaces
{
    public interface IServiceReservacion
    {
        Task<ICollection<ReservacionDTO>> ListAsync();
        Task<ReservacionDTO> FindByIdAsync(int id);
    }
}