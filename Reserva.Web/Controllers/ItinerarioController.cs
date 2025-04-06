using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reserva.Infraestructure.Data;
using Reserva.Infraestructure.Models;

namespace Reserva.Web.Controllers
{
    public class ItinerarioController : Controller
    {
        private readonly ThousandSunnyContext _context;

        public ItinerarioController(ThousandSunnyContext context)
        {
            _context = context;
        }

        // GET: Itinerario
        public async Task<IActionResult> Index()
        {
            var thousandSunnyContext = _context.Itinerario.Include(i => i.IdCruceroNavigation).Include(i => i.IdPuertoNavigation);
            return View(await thousandSunnyContext.ToListAsync());
        }

        // GET: Itinerario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itinerario = await _context.Itinerario
                .Include(i => i.IdCruceroNavigation)
                .Include(i => i.IdPuertoNavigation)
                .FirstOrDefaultAsync(m => m.IdItinerario == id);
            if (itinerario == null)
            {
                return NotFound();
            }

            return View(itinerario);
        }

        // GET: Itinerario/Create
        public IActionResult Create()
        {
            ViewData["IdCrucero"] = new SelectList(_context.Crucero, "IdCrucero", "Nombre");
            ViewData["IdPuerto"] = new SelectList(_context.Puerto, "IdPuerto", "Nombre");
            return View();
        }

        // POST: Itinerario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdItinerario,Dia,Descripcion,IdPuerto,IdCrucero")] Itinerario itinerario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itinerario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCrucero"] = new SelectList(_context.Crucero, "IdCrucero", "Nombre", itinerario.IdCrucero);
            ViewData["IdPuerto"] = new SelectList(_context.Puerto, "IdPuerto", "Nombre", itinerario.IdPuerto);
            return View(itinerario);
        }

        // GET: Itinerario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itinerario = await _context.Itinerario.FindAsync(id);
            if (itinerario == null)
            {
                return NotFound();
            }
            ViewData["IdCrucero"] = new SelectList(_context.Crucero, "IdCrucero", "Nombre", itinerario.IdCrucero);
            ViewData["IdPuerto"] = new SelectList(_context.Puerto, "IdPuerto", "Nombre", itinerario.IdPuerto);
            return View(itinerario);
        }

        // POST: Itinerario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdItinerario,Dia,Descripcion,IdPuerto,IdCrucero")] Itinerario itinerario)
        {
            if (id != itinerario.IdItinerario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itinerario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItinerarioExists(itinerario.IdItinerario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCrucero"] = new SelectList(_context.Crucero, "IdCrucero", "Nombre", itinerario.IdCrucero);
            ViewData["IdPuerto"] = new SelectList(_context.Puerto, "IdPuerto", "Nombre", itinerario.IdPuerto);
            return View(itinerario);
        }

        // GET: Itinerario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itinerario = await _context.Itinerario
                .Include(i => i.IdCruceroNavigation)
                .Include(i => i.IdPuertoNavigation)
                .FirstOrDefaultAsync(m => m.IdItinerario == id);
            if (itinerario == null)
            {
                return NotFound();
            }

            return View(itinerario);
        }

        // POST: Itinerario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itinerario = await _context.Itinerario.FindAsync(id);
            if (itinerario != null)
            {
                _context.Itinerario.Remove(itinerario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItinerarioExists(int id)
        {
            return _context.Itinerario.Any(e => e.IdItinerario == id);
        }

        [HttpGet]
        public IActionResult ObtenerPuertos(int idItinerario)
        {
            var itinerarios = _context.Itinerario
                .Where(i => i.IdCrucero == idItinerario)
                .OrderBy(i => i.Dia)
                .Include(i => i.IdPuertoNavigation)
                .ToList();

            var salida = itinerarios.FirstOrDefault()?.IdPuertoNavigation.Nombre ?? "Desconocido";
            var regreso = itinerarios.LastOrDefault()?.IdPuertoNavigation.Nombre ?? "Desconocido";

            return Json(new { puertoSalida = salida, puertoRegreso = regreso });
        }

    }
}
