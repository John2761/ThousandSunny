using Microsoft.AspNetCore.Mvc;
using Reserva.Application.Services.Interfaces;

namespace Reserva.Web.Controllers
{
    public class ReservacionController : Controller
    {
            private readonly IServiceReservacion _serviceReservacion;

            public ReservacionController(IServiceReservacion serviceReservacion)
            {
                _serviceReservacion = serviceReservacion;
            }


            [HttpGet]
            public async Task<IActionResult> Index()
            {
                var reservaciones = await _serviceReservacion.ListAsync();
                return View(reservaciones);
            }

            [HttpGet]
            public async Task<IActionResult> Detalle(int id)
            {
                var reservaciones = await _serviceReservacion.FindByIdAsync(id);
                if (reservaciones == null) return NotFound();
                return View(reservaciones);
            }
        
    }
}
