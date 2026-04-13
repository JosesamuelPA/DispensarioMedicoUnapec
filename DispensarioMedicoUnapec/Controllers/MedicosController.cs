using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DispensarioMedicoUnapec.Data;
using DispensarioMedicoUnapec.Models;
using Microsoft.AspNetCore.Authorization;

namespace DispensarioMedicoUnapec.Controllers
{
    [Authorize]
    public class MedicosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Medicos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int pageSize = 10;
            int totalItems = await _context.Medicos.CountAsync();
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            return View(await _context.Medicos.Take(pageSize).ToListAsync());
        }

        // GET: Medicos/Filter
        public async Task<IActionResult> Filter(string filter_name, string filter_lastName, string filter_cedula, string filter_tanda, string filter_especialidad, string filter_status, int page = 1)
        {
            int pageSize = 10;
            var medicos = _context.Medicos.AsQueryable();

            if (!string.IsNullOrEmpty(filter_name))
            {
                medicos = medicos.Where(m => m.Nombre.Contains(filter_name));
            }
            if (!string.IsNullOrEmpty(filter_lastName))
            {
                medicos = medicos.Where(m => m.Apellido.Contains(filter_lastName));
            }
            if (!string.IsNullOrEmpty(filter_cedula))
            {
                medicos = medicos.Where(m => m.Cedula.Contains(filter_cedula));
            }
            if (!string.IsNullOrEmpty(filter_especialidad))
            {
                medicos = medicos.Where(m => m.Especialidad.Contains(filter_especialidad));
            }
            if (!string.IsNullOrEmpty(filter_tanda) && filter_tanda != "Todos")
            {
                if (Enum.TryParse<TandaLaboral>(filter_tanda, out var tanda))
                {
                    medicos = medicos.Where(m => m.TandaLaboral == tanda);
                }
            }
            if (!string.IsNullOrEmpty(filter_status) && filter_status != "Todos")
            {
                if (Enum.TryParse<EstadoMedico>(filter_status, out var estado))
                {
                    medicos = medicos.Where(m => m.EstadoMedico == estado);
                }
            }

            int totalItems = await medicos.CountAsync();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pagedResults = await medicos.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return PartialView("_MedicosTable", pagedResults);
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,FechaNacimiento,Cedula,NumeroCarnet,TandaLaboral,Especialidad,EstadoMedico")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Médico registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,FechaNacimiento,Cedula,NumeroCarnet,TandaLaboral,Especialidad,EstadoMedico")] Medico medico)
        {
            if (id != medico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Datos del médico actualizados.";
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico != null)
            {
                _context.Medicos.Remove(medico);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Médico eliminado del sistema.";
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.Id == id);
        }
    }
}
