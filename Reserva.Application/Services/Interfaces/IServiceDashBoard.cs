using Reserva.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.Services.Interfaces
{
    public interface IServiceDashBoard
    {
        Task<List<DashboardDTO>> ObtenerDashboardAsync();
    }
}
