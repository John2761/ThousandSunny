using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Application.Services.Interfaces;
using Reserva.Infraestructure.Models;
using Reserva.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.Services.Implementations
{
    public class ServiceCrucero : IServiceCrucero
    {
        private readonly IRepositoryCrucero _cruceroRepository;
        private readonly IMapper _mapper;

        public ServiceCrucero(IRepositoryCrucero cruceroRepository, IMapper mapper)
        {
            _cruceroRepository = cruceroRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(CruceroDTO cruceroDto)
        {
            var crucero = _mapper.Map<Crucero>(cruceroDto);
            return await _cruceroRepository.AddAsync(crucero);
        }

        public async Task<CruceroDTO?> FindByIdAsync(int id)
        {
            var crucero = await _cruceroRepository.FindByIdAsync(id);
            return _mapper.Map <CruceroDTO>(crucero);

        }

        public async Task<ICollection<CruceroDTO>> ListAsync()
        {
            var crucero = await _cruceroRepository.ListAsync();
            return _mapper.Map<ICollection<CruceroDTO>>(crucero);
        }

        public async Task<List<HabitacionDTO>> ObtenerHabitacionesPorBarcoAsync(int idBarco)
        {
            var habitaciones = await _cruceroRepository.ObtenerHabitacionesPorBarcoAsync(idBarco);
            return _mapper.Map<List<HabitacionDTO>>(habitaciones);
        }
    }
}
