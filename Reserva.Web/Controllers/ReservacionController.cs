using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reserva.Application.Services.Interfaces;
using Reserva.Web.ViewModels;

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

            [HttpGet]
            public async Task<IActionResult> Create()
            {
                var formDto = await _serviceReservacion.GetReservaFormDataAsync();
    
                var viewModel = new ReservacionViewModel
            {
                FechasDisponibles = formDto.Fechas.Select(f => new SelectListItem
                {
                    Value = f.IdFecha.ToString(),
                    Text = $"{f.NombreCrucero} - Salida: {f.FechaSalida:yyyy-MM-dd}"
                }).ToList(),

                Complementos = formDto.Complementos.Select(c => new ComplementoSeleccionadoViewModel
                {
                    IdComplemento = c.IdComplemento,
                    Nombre = c.Nombre,
                    Tipo = c.Tipo,
                    Precio = c.Precio,
                    Cantidad = 0
                }).ToList(),

                Huespedes = Enumerable.Range(0, 3).Select(i => new HuespedInputModel()).ToList(),

                // ✅ AGREGÁS ESTA LÍNEA:
                Fechas = formDto.Fechas
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetItinerariosPorCrucero(int idCrucero, string fechaSalidaStr)
        {
            DateTime? fechaSalida = null;

            if (DateTime.TryParse(fechaSalidaStr, out var salida))
                fechaSalida = salida;

            var itinerarios = await _serviceReservacion.GetItinerariosPorCruceroAsync(idCrucero, fechaSalida);

            var result = itinerarios.Select(i => new
            {
                id = i.IdItinerario,
                nombre = i.Descripcion,
                duracion = i.DuracionDias,
                fechaRegreso = i.FechaRegreso.ToString("yyyy-MM-dd")
            });

            return Json(result);
        }

        [HttpGet]
        public IActionResult RenderComplementoPartial(int index, int idComplemento, string nombre, decimal precio, bool tipo)
        {
            var complemento = new ComplementoSeleccionadoViewModel
            {
                IdComplemento = idComplemento,
                Nombre = nombre,
                Precio = precio,
                Tipo = tipo,
                Cantidad = 0
            };

            ViewData["Index"] = index;
            return PartialView("_ComplementoForm", complemento);
        }


        [HttpGet]
        public IActionResult RenderHuespedPartial(int index)
        {
            var huesped = new HuespedInputModel();
            ViewData["Index"] = index;
            return PartialView("_HuespedesForm", huesped);
        }

        [HttpGet]
        public async Task<IActionResult> GetHabitacionesPorFecha(int idFecha)
        {
            var habitaciones = await _serviceReservacion.GetHabitacionesPorFechaAsync(idFecha);

            var result = habitaciones.Select(h => new
            {
                id = h.IdHabitacion,
                nombre = h.Nombre,
                precio = h.Precio.ToString("C")
            });

            return Json(result);
        }


    }
}
