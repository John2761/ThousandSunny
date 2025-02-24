
using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;

namespace Reserva.Application.Profiles
{
    public class BarcoProfile : Profile
    {
        public BarcoProfile()
        {
            CreateMap<BarcoDTO, Barco>().ReverseMap();

        }

    }
}
