using Microsoft.AspNetCore.Mvc;
using Reserva.Application.Services.Interfaces;

namespace Reserva.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IServiceDashBoard _dashboardService;

        public DashboardController(IServiceDashBoard dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<IActionResult> Index()
        {
            var dashboardData = await _dashboardService.ObtenerDashboardAsync();
            return View(dashboardData);
        }
    }
}
