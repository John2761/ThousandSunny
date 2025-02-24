using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Application.Services.Interfaces;
using Reserva.Infraestructure.Repository.Interfaces;

namespace Reserva.Application.Services.Implementations
{
    internal class ServiceReservacion : IServiceReservacion
    {
        private readonly IRepositoryReservacion _repository;
        private readonly IMapper _mapper;

        public ServiceReservacion(IRepositoryReservacion repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReservacionDTO> FindByIdAsync(int id)
        {
            var Reservacion = await _repository.FindByIdAsync(id);
            return _mapper.Map<ReservacionDTO>(Reservacion);
        }

        public async Task<ICollection<ReservacionDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<ReservacionDTO>>(list); ;
        }
    }
}
