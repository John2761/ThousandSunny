using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Reserva.Application.Services.Interfaces;
using Reserva.Web.Models;

namespace Reserva.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IServiceCrucero _cruceroService;


    public HomeController(ILogger<HomeController> logger, IServiceCrucero cruceroService)
    {
        _logger = logger;
        _cruceroService = cruceroService;
    }

    public async Task<IActionResult> Index()
    {
        var todosLosCruceros = await _cruceroService.ListAsync();

        var crucerosDisponibles = todosLosCruceros
            .Where(c => c.FechasPrecios != null && c.FechasPrecios.Any(fp => fp.FechaSalida >= DateTime.Today))
            .ToList();

        return View(crucerosDisponibles);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
