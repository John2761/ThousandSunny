using Microsoft.AspNetCore.Mvc;
using Reserva.Application.DTOs;
using Reserva.Application.Services.Implementations;
using Reserva.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace Reserva.Web.Controllers
{
    public class CruceroController : Controller
    {
        private readonly IServiceCrucero _serviceCrucero;
        private readonly IServiceBarco _serviceBarco;
        private readonly IServicePuerto _servicePuerto;

        public CruceroController(IServiceCrucero serviceCrucero, IServiceBarco serviceBarco, IServicePuerto servicePuerto)
        {
            _serviceCrucero = serviceCrucero;
            _serviceBarco = serviceBarco;
            _servicePuerto = servicePuerto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cruceros = await _serviceCrucero.ListAsync();
            return View(cruceros);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var crucero = await _serviceCrucero.FindByIdAsync(id);
            if (crucero == null)
            {
                return NotFound();
            }

            // Asignar manualmente el nombre del puerto a cada itinerario
            foreach (var item in crucero.Itinerario)
            {
                var puerto = await _servicePuerto.FindByIdAsync(item.IdPuerto);
                item.NombrePuerto = puerto?.Nombre ?? "Desconocido";
            }
            return View(crucero);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Barcos = await _serviceBarco.ListAsync();
            ViewBag.Puertos = await _servicePuerto.ListAsync();
            ViewBag.Habitaciones = new List<HabitacionDTO>(); // Inicialmente vacío
            return View(new CruceroDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CruceroDTO cruceroDto)
        {
            Console.WriteLine($"Descripcion Recibida: '{cruceroDto.Descripcion}'");
            Console.WriteLine($"Cantidad de Itinerarios: {cruceroDto.Itinerario.Count}");
           
            foreach (var item in cruceroDto.Itinerario)
            {
                Console.WriteLine($"Día: {item.Dia}, PuertoID: {item.IdPuerto}, Descripcion: '{item.Descripcion}'");
            }

            if (!ModelState.IsValid)
            {
                var errores = ModelState
                    .Where(ms => ms.Value.Errors.Any())
                    .Select(ms => $"{ms.Key}: {string.Join(", ", ms.Value.Errors.Select(e => e.ErrorMessage))}")
                    .ToList();

                Console.WriteLine("Errores del formulario:");
                errores.ForEach(e => Console.WriteLine(e));

                ViewBag.ErrorMessage = "Errores en el formulario: " + string.Join(" | ", errores);
                ViewBag.Barcos = await _serviceBarco.ListAsync();
                ViewBag.Puertos = await _servicePuerto.ListAsync();
                return View(cruceroDto);
            }

            try
            {
                // 🔹 NO asignamos manualmente `IdItinerario`, ya que la base de datos lo genera automáticamente.
                await _serviceCrucero.AddAsync(cruceroDto);
                TempData["SuccessMessage"] = "Crucero creado exitosamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al guardar el crucero: " + (ex.InnerException?.Message ?? ex.Message);
                ViewBag.Barcos = await _serviceBarco.ListAsync();
                ViewBag.Puertos = await _servicePuerto.ListAsync();
                return View(cruceroDto);
            }
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerHabitaciones(int idBarco)
        {
            var habitaciones = await _serviceCrucero.ObtenerHabitacionesPorBarcoAsync(idBarco);

            var result = habitaciones.Select(h => new
            {
                id = h.IdHabitacion,
                nombre = h.Nombre,
                precios = h.Precios
            });
             
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string? Destino, string? PuertoSalida, int? Mes, int? Anio, string? OrdenarPor)
        {
            var cruceros = await _serviceCrucero.ListAsync();

            // Filtrado por Destino
            if (!string.IsNullOrEmpty(Destino))
            {
                cruceros = cruceros.Where(c => c.Nombre.Contains(Destino, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Filtrado por Puerto de Salida (desde Itinerario)
            if (!string.IsNullOrEmpty(PuertoSalida))
            {
                cruceros = cruceros.Where(c => c.Itinerario.Any(i => i.NombrePuerto != null && i.NombrePuerto.Contains(PuertoSalida, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            // Filtrado por Mes y Año (desde FechasPrecios)
            if (Mes.HasValue && Anio.HasValue)
            {
                cruceros = cruceros.Where(c => c.FechasPrecios.Any(fp => fp.FechaSalida.Month == Mes && fp.FechaSalida.Year == Anio)).ToList();
            }

            // Ordenamiento
            switch (OrdenarPor)
            {
                case "precioAsc":
                    cruceros = cruceros
                        .Where(c => c.FechasPrecios.Any(fp => fp.PrecioHabitacion.Any()))
                        .OrderBy(c => c.FechasPrecios.Min(fp => fp.PrecioHabitacion.Min(p => p.PrecioHabitacion)))
                        .ToList();
                    break;

                case "precioDesc":
                    cruceros = cruceros
                        .Where(c => c.FechasPrecios.Any(fp => fp.PrecioHabitacion.Any()))
                        .OrderByDescending(c => c.FechasPrecios.Min(fp => fp.PrecioHabitacion.Min(p => p.PrecioHabitacion)))
                        .ToList();
                    break;

                case "fechaAsc":
                    cruceros = cruceros.OrderBy(c => c.FechasPrecios.Min(fp => fp.FechaSalida)).ToList();
                    break;

                default:
                    break;
            }

            return View("Index", cruceros); // Usamos la misma vista Index
        }

    }
}