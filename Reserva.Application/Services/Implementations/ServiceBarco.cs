using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Application.Services.Interfaces;
using Reserva.Infraestructure.Repository.Interfaces;


namespace Reserva.Application.Services.Implementations
{
    public class ServiceBarco : IServiceBarco
    {
        private readonly IRepositoryBarco _repository;
        private readonly IMapper _mapper;

        public ServiceBarco(IRepositoryBarco repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BarcoDTO> FindByIdAsync(int id)
        {
            var Barco = await _repository.FindByIdAsync(id);
            return _mapper.Map<BarcoDTO>(Barco);
        }

        public async Task<ICollection<BarcoDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<BarcoDTO>>(list); ;
        }
    }
}
