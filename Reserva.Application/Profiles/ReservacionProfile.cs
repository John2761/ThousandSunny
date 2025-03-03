
using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;

namespace Reserva.Application.Profiles
{
    public class ReservacionProfile : Profile
    {
        public ReservacionProfile()
        {
            CreateMap<Reservacion, ReservacionDTO>()
                 .ForMember(dest => dest.NombreCrucero, opt => opt.MapFrom(src => src.IdCruceroNavigation.Nombre))
                 .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => src.IdFechaNavigation.FechaSalida))
                 .ForMember(dest => dest.CantidadDias, opt => opt.MapFrom(src => src.IdCruceroNavigation.CantidadDias))
                 .ForMember(dest => dest.Habitaciones, opt => opt.MapFrom(src => src.DetalleReservacion))
                 .ForMember(dest => dest.Complementos, opt => opt.MapFrom(src => src.ReservaComplemento))
                 .ReverseMap();

            CreateMap<DetalleReservacion, HabitacionesReservaDTO>()
                .ForMember(dest => dest.NombreHabitacion, opt => opt.MapFrom(src => src.IdHabitacionNavigation.Nombre))
                .ForMember(dest => dest.CantidadHuespedes, opt => opt.MapFrom(src => src.CantHuespedes))
                .ReverseMap();

            CreateMap<ReservaComplemento, ComplementoReservaDTO>()
                .ForMember(dest => dest.NombreComplemento, opt => opt.MapFrom(src => src.IdComplementoNavigation.Nombre))
                .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
                .ReverseMap();
        }
    }
}
