using AutoMapper;
using Reserva.Application.DTOs;
using Reserva.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.Profiles
{
    public class PuertoProfile : Profile
    {
        public PuertoProfile()
        {
            CreateMap<Puerto, PuertoDTO>().ReverseMap();
        }
    }
}
