using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;


namespace Reserva.Application.Profiles
{
    public class HabitacionProfile : Profile
    {
        public HabitacionProfile() 
        {
            CreateMap<HabitacionDTO, Habitacion>().ReverseMap();

        }
        
    }
}
