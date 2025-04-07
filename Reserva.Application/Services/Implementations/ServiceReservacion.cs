
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reserva.Application.DTOs;
using Reserva.Application.Services.Interfaces;
using Reserva.Infraestructure.Repository.Interfaces;

namespace Reserva.Application.Services.Implementations
{
    public class ServiceReservacion : IServiceReservacion
    {
        private readonly IRepositoryReservacion _repository;
        private readonly IMapper _mapper;

        public ServiceReservacion(IRepositoryReservacion repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReservacionDTO?> FindByIdAsync(int id)
        {
            var Reservacion = await _repository.FindByIdAsync(id);
            return _mapper.Map<ReservacionDTO>(Reservacion);
        }

        public async Task<ICollection<ReservacionDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<ReservacionDTO>>(list); ;
        }
        public async Task<ReservacionFormDTO> GetReservaFormDataAsync()
        {
            var fechas = await _repository.ListFechasAsync();
            var complementos = await _repository.ListComplementosAsync();

            var dto = new ReservacionFormDTO
            {
                Fechas = fechas.Select(f => new FechaDTO
                {
                    IdFecha = f.IdFecha,
                    FechaSalida = f.FechaSalida,
                    IdCrucero = f.IdCrucero,
                    NombreCrucero = f.IdCruceroNavigation.Nombre
                }).ToList(),

                Complementos = complementos.Select(c => new ComplementoDTO
                {
                    IdComplemento = c.IdComplemento,
                    Nombre = c.Nombre,
                    Tipo = c.TipoAplicacion,
                    Precio = c.Precio
                }).ToList()
            };
            return dto;
        }

        public async Task<List<ItinerarioInfoDTO>> GetItinerariosPorCruceroAsync(int idCrucero, DateTime? fechaSalida = null)
        {
            var itinerarios = await _repository.GetItinerariosPorIdCruceroAsync(idCrucero);

            if (!itinerarios.Any()) return new List<ItinerarioInfoDTO>();

            return itinerarios
            .GroupBy(i => i.IdItinerario)
        .   Select(g => new ItinerarioInfoDTO
            {
                IdItinerario = g.Key,
                Descripcion = g.First().Descripcion,
                DuracionDias = g.Max(i => i.Dia),
                FechaRegreso = fechaSalida.HasValue
                ? fechaSalida.Value.AddDays(g.Max(i => i.Dia))
                : DateTime.MinValue // si no querés calcularlo
            })
        .ToList();
        }

        public async Task<List<HabitacionDTO>> GetHabitacionesPorFechaAsync(int idFecha)
        {
            var precios = await _repository.GetPreciosPorFechaAsync(idFecha);

            var preciosConDTO = precios
                .Select(p => new
                {
                    Habitacion = p.IdHabitacionNavigation,
                    Precio = new PrecioHabitacionDTO
                    {
                        NombreHabitacion = p.IdHabitacionNavigation.Nombre,
                        PrecioHabitacion = p.PrecioHabitacion
                    }
                })
                .ToList();

            var agrupado = preciosConDTO
                .GroupBy(p => p.Habitacion.IdHabitacion)
                .Select(g => new HabitacionDTO
                {
                    IdHabitacion = g.Key,
                    Nombre = g.First().Habitacion.Nombre,
                    Descripcion = g.First().Habitacion.Descripcion,
                    Tamaño = g.First().Habitacion.Tamaño,
                    HuespedesMin = g.First().Habitacion.HuespedesMin,
                    HuespedesMax = g.First().Habitacion.HuespedesMax,
                    Precios = g.Select(x => x.Precio).ToList()
                })
                .ToList();

            return agrupado;
        }


    }
}