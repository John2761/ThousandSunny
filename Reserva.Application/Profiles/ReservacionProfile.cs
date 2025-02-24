
using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;

namespace Reserva.Application.Profiles
{
    public class ReservacionProfile : Profile
    {
        public ReservacionProfile()
        {
            CreateMap<ReservacionDTO,Reservacion>().ReverseMap();
        }
    }
}
