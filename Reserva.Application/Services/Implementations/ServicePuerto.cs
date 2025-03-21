using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Application.Services.Interfaces;
using Reserva.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.Services.Implementations
{
    public class ServicePuerto : IServicePuerto
    {
        private readonly IRepositoryPuerto _puertoRepository;
        private readonly IMapper _mapper;

        public ServicePuerto(IRepositoryPuerto puertoRepository, IMapper mapper)
        {
            _puertoRepository = puertoRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<PuertoDTO>> ListAsync()
        {
            var puertos = await _puertoRepository.ListAsync();
            return _mapper.Map<ICollection<PuertoDTO>>(puertos);
        }

        public async Task<PuertoDTO?> FindByIdAsync(int id)
        {
            var puerto = await _puertoRepository.FindByIdAsync(id);
            if (puerto == null) return null;

            return new PuertoDTO
            {
                IdPuerto = puerto.IdPuerto,
                Nombre = puerto.Nombre
            };
        }
    }
}
