using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            TempData["CartShopping"] = null;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BarcoDTO barcoDTO)
        {

             if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = string.Join("; ", ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));
                return View(barcoDTO);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync()) 
            {
                try
                {
                    // Crear el barco sin asignar `idBarco`
                    var barco = new Barco
                    {
                        Nombre = barcoDTO.Nombre,
                        Descripcion = barcoDTO.Descripcion
                    };

                    await _context.Barco.AddAsync(barco);
                    await _context.SaveChangesAsync();  // Barco.IdBarco` tiene el ID generado

                    // Obtener habitaciones guardadas en `TempData`
                    var jsonHabitaciones = TempData["CartShopping"] as string;
                    List<BarcoHabitacionDTO>? habitacionesDTO = jsonHabitaciones != null
                        ? JsonConvert.DeserializeObject<List<BarcoHabitacionDTO>>(jsonHabitaciones)
                        : new List<BarcoHabitacionDTO>();

                    // Agregar habitaciones vinculadas con el `IdBarco` generado
                    if (habitacionesDTO != null && habitacionesDTO.Any())
                    {
                        foreach (var habitacionDTO in habitacionesDTO)
                        {
                            _context.BarcoHabitacion.Add(new BarcoHabitacion
                            {
                                IdBarco = barco.IdBarco, // Ahora tiene un ID válido
                                IdHabitacion = habitacionDTO.idHabitacion,
                                CantHabitaciones = habitacionDTO.CantHabitaciones
                            });
                        }
                        await _context.SaveChangesAsync();
                    }
                    await transaction.CommitAsync(); // Confirmar cambios en la BD
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError(string.Empty, "Error al guardar el barco: " + ex.Message);
                    return View(barcoDTO);
                }
            }
            return RedirectToAction("Index");
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
        public async Task<IActionResult> GetHabitacionesAgregadas(int idBarco) // tabla cargada
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
            // Obtener habitaciones actuales de TempData
            var json = TempData["CartShopping"] as string;
            List<BarcoHabitacionDTO> habitaciones = json != null
                ? JsonConvert.DeserializeObject<List<BarcoHabitacionDTO>>(json)
                : new List<BarcoHabitacionDTO>();

            // Agregar nueva habitación
            var habitacionExistente = habitaciones.FirstOrDefault(h => h.idHabitacion == idHabitacion);
            if (habitacionExistente != null)
            {
                habitacionExistente.CantHabitaciones += cantidad;
            }
            else
            {
                habitaciones.Add(new BarcoHabitacionDTO
                {
                    idHabitacion = idHabitacion,
                    CantHabitaciones = cantidad
                });
            }

            // Guardar de nuevo en TempData
            TempData["CartShopping"] = JsonConvert.SerializeObject(habitaciones);

            return PartialView("_DetalleHabitaciones", habitaciones);
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

            await _serviceBarco.UpdateAsync(barco);
            return RedirectToAction(nameof(Index));
        }

    }
}
