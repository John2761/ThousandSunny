
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
                 .ForMember(dest => dest.NombreBarco, opt => opt.MapFrom(src => src.IdCruceroNavigation.IdBarcoNavigation.Nombre)) 
                 .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => src.IdFechaNavigation.FechaSalida))
                 .ForMember(dest => dest.CantidadDias, opt => opt.MapFrom(src => src.IdCruceroNavigation.CantidadDias))

                 .ForMember(dest => dest.FechaLimitePago, opt => opt.MapFrom(src =>
                    src.FechaLimite == DateTime.MinValue ? (DateTime?)null : src.FechaLimite))

                
                .ForMember(dest => dest.MontoPendiente, opt => opt.MapFrom(src =>
                    src.IdDatosPagoNavigation != null ? src.IdDatosPagoNavigation.MontoPendiente ?? 0 : 0))

    .ForMember(dest => dest.PuertoSalida, opt => opt.MapFrom(src =>
        src.IdCruceroNavigation.Itinerario.OrderBy(i => i.Dia).FirstOrDefault().IdPuertoNavigation.Nombre))

    
    .ForMember(dest => dest.PuertoRegreso, opt => opt.MapFrom(src =>
        src.IdCruceroNavigation.Itinerario.OrderByDescending(i => i.Dia).FirstOrDefault().IdPuertoNavigation.Nombre))

    .ForMember(dest => dest.EstadoPago, opt => opt.MapFrom(src =>
    src.IdDatosPagoNavigation != null && src.IdDatosPagoNavigation.MontoPendiente.HasValue && src.IdDatosPagoNavigation.MontoPendiente > 0
        ? "Pendiente"
        : "Pagado"))

                 .ForMember(dest => dest.Habitaciones, opt => opt.MapFrom(src => src.DetalleReservacion))
                 .ForMember(dest => dest.Complementos, opt => opt.MapFrom(src => src.ReservaComplemento))

                 .ForMember(dest => dest.TotalHabitaciones, opt => opt.MapFrom(src =>
                 src.DetalleReservacion.Sum(d => d.IdHabitacionNavigation.Precio
                 .Where(p => p.IdFecha == src.IdFecha) // Filtrar por fecha de la reservación
                 .Select(p => p.PrecioHabitacion)
                 .FirstOrDefault() * d.CantHuespedes)))
                 
            .ForMember(dest => dest.TotalComplementos, opt => opt.MapFrom(src =>
                src.ReservaComplemento.Sum(c => c.Cantidad *
                    (c.IdComplementoNavigation.Precio)))) 

            .ReverseMap();



            CreateMap<DetalleReservacion, HabitacionesReservaDTO>()
                .ForMember(dest => dest.NombreHabitacion, opt => opt.MapFrom(src => src.IdHabitacionNavigation.Nombre))
                .ForMember(dest => dest.CantidadHuespedes, opt => opt.MapFrom(src => src.CantHuespedes))
                .ForMember(dest => dest.PrecioTotal, opt => opt.MapFrom(src =>
        src.IdHabitacionNavigation.Precio
            .Where(p => p.IdFecha == src.IdReservacionNavigation.IdFecha)
            .Select(p => p.PrecioHabitacion)
            .FirstOrDefault() * src.CantHuespedes))
                .ReverseMap();

            CreateMap<ReservaComplemento, ComplementoReservaDTO>()
                .ForMember(dest => dest.NombreComplemento, opt => opt.MapFrom(src => src.IdComplementoNavigation.Nombre))
                .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
                .ForMember(dest => dest.PrecioTotal, opt => opt.MapFrom(src => src.Cantidad * src.IdComplementoNavigation.Precio))
                .ReverseMap();
        }
    }
}
