using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Reserva.Application.DTOs;
using Reserva.Application.Services.Interfaces;
using Reserva.Infraestructure.Models;
using Reserva.Web.Util;

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

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new HabitacionDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(HabitacionDTO habitacionDto)
        {
           if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = string.Join("; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));
                return View(habitacionDto);
            }

            try
            {
                await _serviceHabitacion.AddAsync(habitacionDto);

                // ✅ SWEETALERT - GUARDAR MENSAJE EN TempData ✅
                TempData["SuccessMessage"] = SweetAlertHelper.Mensaje("Éxito", "Habitación agregada correctamente", SweetAlertMessageType.success);

                return RedirectToAction("Index"); // 🔹 REDIRIGE AL INDEX
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al guardar la habitación: " + ex.InnerException?.Message ?? ex.Message;
                return View(habitacionDto);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var habitacion = await _serviceHabitacion.FindByIdAsync(id);
            if (habitacion == null) return NotFound();
            return View(habitacion);
        }

        
        [HttpPost]
        public async Task<IActionResult> Editar(HabitacionDTO habitacionDto)
        {
            if (!ModelState.IsValid) 
            {
                ViewBag.ErrorMessage = string.Join("; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));
                return View(habitacionDto);
            }

            try
            {
                await _serviceHabitacion.UpdateAsync(habitacionDto);

                // ✅ SWEETALERT - GUARDAR MENSAJE EN TempData ✅
                TempData["SuccessMessage"] = SweetAlertHelper.Mensaje("Éxito", "Habitación actualizada correctamente", SweetAlertMessageType.success);

                return RedirectToAction("Index"); // 🔹 REDIRIGE AL INDEX
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al actualizar la habitación: " + ex.InnerException?.Message ?? ex.Message;
                return View(habitacionDto);
            }
        }
    }
}
