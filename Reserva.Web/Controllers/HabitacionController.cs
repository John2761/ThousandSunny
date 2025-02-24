using Microsoft.AspNetCore.Mvc;
using Reserva.Application.Services.Interfaces;

namespace Reserva.Web.Controllers
{
    public class HabitacionController : Controller
    {
        private readonly IServiceHabitacion _serviceHabitacion;

        public HabitacionController(IServiceHabitacion serviceHabitacion)
        {
            _serviceHabitacion = serviceHabitacion;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var habitaciones = await _serviceHabitacion.ListAsync();
            return View(habitaciones);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var habitaciones = await _serviceHabitacion.FindByIdAsync(id);
            if (habitaciones == null) return NotFound();
            return View(habitaciones);
        }
    }
}
