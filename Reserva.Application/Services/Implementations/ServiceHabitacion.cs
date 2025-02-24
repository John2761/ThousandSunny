using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Application.Services.Interfaces;
using Reserva.Infraestructure.Repository.Interfaces;

namespace Reserva.Application.Services.Implementations
{
    public class ServiceHabitacion : IServiceHabitacion
    {
        private readonly IRepositoryHabitacion _repository;
        private readonly IMapper _mapper;

        public ServiceHabitacion(IRepositoryHabitacion repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<HabitacionDTO> FindByIdAsync(int id)
        {
            var habitacion = await _repository.FindByIdAsync(id);
            return _mapper.Map<HabitacionDTO>(habitacion);
        }

        public async Task<ICollection<HabitacionDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<HabitacionDTO>>(list); ;
        }
    }
}
