
using AutoMapper;
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
            var data = await _repository.GetHabitacionesConPrecioPorFechaAsync(idFecha);

            return data.Select(d => new HabitacionDTO
            {
                IdHabitacion = d.habitacion.IdHabitacion,
                Nombre = d.habitacion.Nombre,
                Precio = d.precio
            }).ToList();
        }


    }
}