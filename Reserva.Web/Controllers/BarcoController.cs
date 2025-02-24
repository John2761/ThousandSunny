using Microsoft.AspNetCore.Mvc;
using Reserva.Application.Services.Interfaces;

namespace Reserva.Web.Controllers
{
    public class BarcoController : Controller
    {
        private readonly IServiceBarco _serviceBarco;

        public BarcoController(IServiceBarco serviceBarco)
        {
            _serviceBarco = serviceBarco;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Barcos = await _serviceBarco.ListAsync();
            return View(Barcos);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var Barcos = await _serviceBarco.FindByIdAsync(id);
            if (Barcos == null) return NotFound();
            return View(Barcos);
        }
    }
}
