using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.Profiles
{
    public class CruceroProfile : Profile
    {
        public CruceroProfile()
        {
            // Mapeo de Crucero a CruceroDTO
            CreateMap<Crucero, CruceroDTO>()
                .ForMember(dest => dest.NombreBarco, opt => opt.MapFrom(c => c.IdBarcoNavigation.Nombre))
                // La imagen se asigna manualmente, por lo que se ignora en el mapeo
                .ForMember(dest => dest.Itinerario, opt => opt.MapFrom(c => c.Itinerario))
                .ForMember(dest => dest.FechasPrecios, opt => opt.MapFrom(c => c.Fecha))
                .ReverseMap();

            // Mapeo de Itinerario a ItinerarioDTO, usando el DTO de Puerto para el puerto asociado
            CreateMap<Itinerario, ItinerarioDTO>()
                .ForMember(dest => dest.Puerto, opt => opt.MapFrom(i => i.IdPuertoNavigation))
                .ReverseMap();

            // Mapeo de Puerto a PuertoDTO (tal como lo definiste)
            CreateMap<Puerto, PuertoDTO>().ReverseMap();

            // Mapeo de Fecha a FechaPrecioDTO, que agrupa la fecha de salida y la lista de precios
            CreateMap<Fecha, FechaPrecioDTO>()
                .ForMember(dest => dest.FechaSalida, opt => opt.MapFrom(f => f.FechaSalida))
                .ForMember(dest => dest.PrecioHabitacion, opt => opt.MapFrom(f => f.Precio))
                .ReverseMap();

            // Mapeo de Precio a PrecioHabitacionDTO, utilizando la navegación a Habitacion
            CreateMap<Precio, PrecioHabitacionDTO>()
                .ForMember(dest => dest.NombreHabitacion, opt => opt.MapFrom(p => p.IdHabitacionNavigation.Nombre))
                .ForMember(dest => dest.PrecioHabitacion, opt => opt.MapFrom(p => p.PrecioHabitacion))
                .ReverseMap();
        }
    }
}
