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

        public async Task<int> AddAsync(HabitacionDTO habitacionDto)
        {
            //validar si el nombre ya existe
            var existe = await _repository.ExisteNombreAsync(habitacionDto.Nombre);
            if (existe)
            {
                throw new ArgumentException("El nombre de la habitación ya esta en uso. Debe ser único.");
                    
            }

            if (habitacionDto.HuespedesMin > habitacionDto.HuespedesMax )
            {
                throw new ArgumentException("El número mínimo de huéspedes no puede ser mayor al máximo.");
            }

            if (!EsTamanoValido(habitacionDto.Tamaño))
            {
                throw new ArgumentException("El tamaño ingresado no es válido.");
            }

            var habitacion = _mapper.Map<Habitacion>(habitacionDto);
            var nuevaHabitacion = await _repository.AddAsync(habitacion);
            return nuevaHabitacion;
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

        public async Task<bool> UpdateAsync(HabitacionDTO habitacionDto)
        {
            var habitacion = _mapper.Map<Habitacion>(habitacionDto);
            return await _repository.UpdateAsync(habitacion);
        }

        private bool EsTamanoValido(string nombre)
        {
            string[] tamanos = { "Pequeña", "Mediana", "Grande", "Extra Grande" };
            return tamanos.Contains(nombre);
        }
    }
}
