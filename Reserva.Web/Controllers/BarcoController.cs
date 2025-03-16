using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reserva.Application.DTOs;
using Reserva.Application.Services.Interfaces;
using Reserva.Infraestructure.Data;
using Reserva.Infraestructure.Models;

namespace Reserva.Web.Controllers
{
    public class BarcoController : Controller
    {
        private readonly IServiceBarco _serviceBarco;
        private readonly ThousandSunnyContext _context;

        public BarcoController(IServiceBarco serviceBarco, ThousandSunnyContext sunnyContext)
        {
            _serviceBarco = serviceBarco;
            _context = sunnyContext;
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

        [HttpGet]
        public IActionResult Create()
        {
            var model = new BarcoDTO
            {
                Habitaciones = new List<BarcoHabitacionDTO>() // Asegurar que no sea null
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(BarcoDTO barcoDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(barcoDTO);
            }

            // Crear el barco
            var barco = new Barco
            {
                Nombre = barcoDTO.Nombre,
                Descripcion = barcoDTO.Descripcion,
                BarcoHabitacion = new List<BarcoHabitacion>()
            };

            _context.Barco.Add(barco);
            _context.SaveChanges(); 

            // Agregar habitaciones al barco (si existen)
            if (barcoDTO.Habitaciones != null)
            {
                foreach (var habitacionDTO in barcoDTO.Habitaciones)
                {
                    barco.BarcoHabitacion.Add(new BarcoHabitacion
                    {
                        IdBarco = barco.IdBarco, // 
                        IdHabitacion = habitacionDTO.idHabitacion,
                        CantHabitaciones = habitacionDTO.CantHabitaciones
                    });
                }

                _context.SaveChanges(); // Guardar las habitaciones
            }

            return RedirectToAction("Index"); // Redirige al listado tras guardar
        }


        [HttpGet]
        public async Task<IActionResult> GetHabitacionesDisponibles()
        {
            var habitaciones = await _context.Habitacion
                .Select(h => new HabitacionDTO
                {
                    IdHabitacion = h.IdHabitacion,
                    Nombre = h.Nombre,
                    Descripcion = h.Descripcion,
                    HuespedesMax = h.HuespedesMax
                }).ToListAsync();

            if (!habitaciones.Any())
            {
                return PartialView("_AddHabitacion", new List<HabitacionDTO>()); // Evita null
            }

            return PartialView("_AddHabitacion", habitaciones);
        }


        [HttpGet]
        public async Task<IActionResult> GetHabitacionesAgregadas(int idBarco)
        {
            var habitaciones = await _context.BarcoHabitacion
                .Include(bh => bh.IdHabitacion)
                .Where(bh => bh.IdBarco == idBarco)
                .ToListAsync();

            return PartialView("_DetalleHabitaciones", habitaciones);
        }


        [HttpPost]
        public IActionResult AgregarHabitacionABarco(int idBarco, int idHabitacion, int cantidad)
        {
            // Obtener el barco con sus habitaciones asociadas
            var barco = _context.Barco
                .Include(b => b.BarcoHabitacion)
                    .ThenInclude(bh => bh.IdHabitacionNavigation) // Uso correcto de navegación
                .FirstOrDefault(b => b.IdBarco == idBarco);

            if (barco == null)
                return NotFound();

            // Verificar si la habitación ya está asociada al barco
            var existente = barco.BarcoHabitacion.FirstOrDefault(bh => bh.IdHabitacion == idHabitacion);
            if (existente != null)
            {
                existente.CantHabitaciones += cantidad;
            }
            else
            {
                barco.BarcoHabitacion.Add(new BarcoHabitacion
                {
                    IdBarco = idBarco,
                    IdHabitacion = idHabitacion,
                    CantHabitaciones = cantidad
                });
            }

            _context.SaveChanges();

            // Convertir la lista intermedia a HabitacionDTO para actualizar la vista
            var habitacionesDTO = barco.BarcoHabitacion.Select(bh => new HabitacionDTO
            {
                IdHabitacion = bh.IdHabitacionNavigation.IdHabitacion,
                Nombre = bh.IdHabitacionNavigation.Nombre,
                Descripcion = bh.IdHabitacionNavigation.Descripcion,
                HuespedesMax = bh.IdHabitacionNavigation.HuespedesMax // Corrección del campo
            }).ToList();

            return PartialView("_DetalleHabitaciones", habitacionesDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, BarcoDTO barco)
        {
            if (id != barco.IdBarco)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(barco);
            }

            await _serviceBarco.UpdateAsync(id, barco);
            return RedirectToAction(nameof(Index));
        }

    }
}
