using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Application.Services.Interfaces;
using Reserva.Infraestructure.Models;
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
            if (!EsTamanoValido(habitacion.Tamaño))
            {
                throw new ArgumentException($"El tamaño '{habitacion.Tamaño}' no es válido");
            }
            return _mapper.Map<HabitacionDTO>(habitacion);
        }

        public async Task<ICollection<HabitacionDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            foreach (var habitacion in list)
            {
                if (!EsTamanoValido(habitacion.Tamaño))
                {
                    throw new ArgumentException($"El tamaño '{habitacion.Tamaño}' no es válido");
                }
            }
                
            return _mapper.Map<ICollection<HabitacionDTO>>(list); ;
        }

        private bool EsTamanoValido(string nombre)
        {
            string[] tamanos = { "Pequeña", "Mediana", "Grande", "Extra Grande" };
            return tamanos.Contains(nombre);
        }
    }
}
