using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Application.Services.Interfaces;
using Reserva.Infraestructure.Models;
using Reserva.Infraestructure.Repository.Implementations;
using Reserva.Infraestructure.Repository.Interfaces;


namespace Reserva.Application.Services.Implementations
{
    public class ServiceBarco : IServiceBarco
    {
        private readonly IRepositoryHabitacion _repositoryHabitacion;
        private readonly IRepositoryBarco _repositoryBarco;
        private readonly IMapper _mapper;

        public ServiceBarco(IRepositoryBarco repositoryBarco, IRepositoryHabitacion repositoryHabitacion, IMapper mapper)
        {
            _repositoryBarco = repositoryBarco;
            _repositoryHabitacion = repositoryHabitacion;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(BarcoHabitacionDTO dto)
        {
            // Validar Stock disponible
            foreach (var item in dto.BarcoNavigation.BarcoHabitacion)
            {
                var Habitacion = await _repositoryHabitacion.FindByIdAsync(item.IdBarcoNavigation.IdBarco);

                if (dto.CantHabitaciones < 1)
                {
                    throw new Exception($"No hay habitaciones disponibles para el Barco {dto.BarcoNavigation.Nombre}, cantidad en stock {item.CantHabitaciones} ");
                }
            }

            var @object = _mapper.Map<BarcoHabitacion>(dto);
            return await _repositoryBarco.AddAsync(@object);
        }

        public async Task<BarcoDTO> FindByIdAsync(int id)
        {
            var Barco = await _repositoryBarco.FindByIdAsync(id);
            return _mapper.Map<BarcoDTO>(Barco);
        }

        public async Task<ICollection<BarcoDTO>> ListAsync()
        {
            var list = await _repositoryBarco.ListAsync();
            return _mapper.Map<ICollection<BarcoDTO>>(list); ;
        }

        public async Task<int> GetNextNumberBarco()
        {
            int nextBarco = await _repositoryBarco.GetNextNumberBarco();
            return nextBarco + 1;
        }
        public async Task<bool> UpdateAsync(BarcoDTO dto)
        {
            //Obtenga el modelo original a actualizar
            var @object = await _repositoryBarco.FindByIdAsync(dto.IdBarco);
            // source, destination
            var entity = _mapper.Map(dto, @object!);
            return await _repositoryBarco.UpdateAsync(entity);
        }
    }
}
