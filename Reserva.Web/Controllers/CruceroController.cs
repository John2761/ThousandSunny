using Microsoft.AspNetCore.Mvc;
using Reserva.Application.Services.Interfaces;

namespace Reserva.Web.Controllers
{
    public class CruceroController : Controller
    {
        private readonly IServiceCrucero _serviceCrucero;

        public CruceroController(IServiceCrucero serviceCrucero)
        {
            _serviceCrucero = serviceCrucero;
        }


        public async Task<IActionResult> Index()
        {
            var crucero = await _serviceCrucero.ListAsync();
            return View(crucero);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var crucero = await _serviceCrucero.FindByIdAsync(id);
            if (crucero == null) 
            {
                return NotFound();
            }
            return View(crucero);
        }
    }
}
