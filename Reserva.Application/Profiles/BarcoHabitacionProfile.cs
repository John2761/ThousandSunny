
using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;
namespace Reserva.Application.Profiles
{
    public class BarcoHabitacionProfile : Profile
    {
        public BarcoHabitacionProfile() {
            CreateMap<BarcoHabitacion, BarcoHabitacionDTO>()
            .ForMember(dest => dest.HabitacionNavigation, opt => opt.MapFrom(src => src.IdHabitacionNavigation))
            .ForMember(dest => dest.NombreHabitacion, opt => opt.MapFrom(src => src.IdHabitacionNavigation.Nombre))  // 👀 Agregar esta línea
            .ForMember(dest => dest.HuespedesMax, opt => opt.MapFrom(src => src.IdHabitacionNavigation.HuespedesMax));

        }
    }
}
