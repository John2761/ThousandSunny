
using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;

namespace Reserva.Application.Profiles
{
    public class BarcoProfile : Profile
    {
        public BarcoProfile()
        {
            CreateMap<Barco, BarcoDTO>()
            .ForMember(dest => dest.Habitaciones, opt => opt.MapFrom(src => src.BarcoHabitacion
                .Select(bh => new HabitacionBarcoDTO
                {
                    NombreHabitacion = bh.IdHabitacionNavigation.Nombre,
                    CantidadHabitaciones = bh.CantHabitaciones,
                    HuespedesMax = bh.IdHabitacionNavigation.HuespedesMax
                }).ToList()))

            // Calcular la capacidad total de huéspedes del barco
            .ForMember(dest => dest.CapacidadTotalHuespedes, opt => opt.MapFrom(src =>
                src.BarcoHabitacion.Sum(bh => bh.CantHabitaciones * bh.IdHabitacionNavigation.HuespedesMax)))

            .ReverseMap();
        }

    }
}
