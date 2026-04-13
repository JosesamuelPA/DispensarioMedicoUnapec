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
    public class Ubicacion_MedicamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Ubicacion_MedicamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ubicacion_Medicamentos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int pageSize = 10;
            var query = _context.Ubicacion_Medicamentos
                                .Include(u => u.Estante)
                                .Include(u => u.Medicamento);
            int totalItems = await query.CountAsync();
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            return View(await query.Take(pageSize).ToListAsync());
        }

        // GET: Ubicacion_Medicamentos/Filter
        public async Task<IActionResult> Filter(string filter_estante, string filter_tramo, string filter_estado, int page = 1)
        {
            int pageSize = 10;
            var ubicaciones = _context.Ubicacion_Medicamentos
                                      .Include(u => u.Estante)
                                      .Include(u => u.Medicamento)
                                      .AsQueryable();

            if (!string.IsNullOrEmpty(filter_estante))
            {
                ubicaciones = ubicaciones.Where(m => m.Estante != null && m.Estante.Nombre.Contains(filter_estante));
            }
            if (!string.IsNullOrEmpty(filter_tramo) && filter_tramo != "Todos")
            {
                if (Enum.TryParse<TramoEstante>(filter_tramo, out var tramo))
                {
                    ubicaciones = ubicaciones.Where(m => m.Tramo == tramo);
                }
            }
            if (!string.IsNullOrEmpty(filter_estado) && filter_estado != "Todos")
            {
                if (Enum.TryParse<EstadoMedicamento>(filter_estado, out var estado))
                {
                    ubicaciones = ubicaciones.Where(m => m.EstadoMedicamento == estado);
                }
            }

            int totalItems = await ubicaciones.CountAsync();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pagedResults = await ubicaciones.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return PartialView("_UbicacionesTable", pagedResults);
        }

        // GET: Ubicacion_Medicamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacion_Medicamento = await _context.Ubicacion_Medicamentos
                .Include(u => u.Estante)
                .Include(u => u.Medicamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ubicacion_Medicamento == null)
            {
                return NotFound();
            }

            return View(ubicacion_Medicamento);
        }

        // GET: Ubicacion_Medicamentos/Create
        public IActionResult Create()
        {
            ViewBag.Estantes = new SelectList(_context.Estantes, "Id", "Nombre");
            ViewBag.Medicamentos = new SelectList(_context.Medicamentos, "Id", "Nombre");
            return View();
        }

        // POST: Ubicacion_Medicamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EstanteId,MedicamentoId,Tramo,Celda,EstadoMedicamento")] Ubicacion_Medicamento ubicacion_Medicamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ubicacion_Medicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Estantes = new SelectList(_context.Estantes, "Id", "Nombre", ubicacion_Medicamento.EstanteId);
            ViewBag.Medicamentos = new SelectList(_context.Medicamentos, "Id", "Nombre", ubicacion_Medicamento.MedicamentoId);
            return View(ubicacion_Medicamento);
        }

        // GET: Ubicacion_Medicamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacion_Medicamento = await _context.Ubicacion_Medicamentos.FindAsync(id);
            if (ubicacion_Medicamento == null)
            {
                return NotFound();
            }
            ViewBag.Estantes = new SelectList(_context.Estantes, "Id", "Nombre", ubicacion_Medicamento.EstanteId);
            ViewBag.Medicamentos = new SelectList(_context.Medicamentos, "Id", "Nombre", ubicacion_Medicamento.MedicamentoId);
            return View(ubicacion_Medicamento);
        }

        // POST: Ubicacion_Medicamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstanteId,MedicamentoId,Tramo,Celda,EstadoMedicamento")] Ubicacion_Medicamento ubicacion_Medicamento)
        {
            if (id != ubicacion_Medicamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ubicacion_Medicamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Ubicacion_MedicamentoExists(ubicacion_Medicamento.Id))
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
            ViewBag.Estantes = new SelectList(_context.Estantes, "Id", "Nombre", ubicacion_Medicamento.EstanteId);
            ViewBag.Medicamentos = new SelectList(_context.Medicamentos, "Id", "Nombre", ubicacion_Medicamento.MedicamentoId);
            return View(ubicacion_Medicamento);
        }

        // GET: Ubicacion_Medicamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacion_Medicamento = await _context.Ubicacion_Medicamentos
                .Include(u => u.Estante)
                .Include(u => u.Medicamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ubicacion_Medicamento == null)
            {
                return NotFound();
            }

            return View(ubicacion_Medicamento);
        }

        // POST: Ubicacion_Medicamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ubicacion_Medicamento = await _context.Ubicacion_Medicamentos.FindAsync(id);
            if (ubicacion_Medicamento != null)
            {
                _context.Ubicacion_Medicamentos.Remove(ubicacion_Medicamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Ubicacion_MedicamentoExists(int id)
        {
            return _context.Ubicacion_Medicamentos.Any(e => e.Id == id);
        }

        // GET: Ubicacion_Medicamentos/GetOccupiedCells?estanteId=1&excludeId=3
        [HttpGet]
        public async Task<IActionResult> GetOccupiedCells(int estanteId, int? excludeId = null)
        {
            var query = _context.Ubicacion_Medicamentos
                                .Include(u => u.Medicamento)
                                .Where(u => u.EstanteId == estanteId);

            if (excludeId.HasValue)
                query = query.Where(u => u.Id != excludeId.Value);

            var occupied = await query.Select(u => new
            {
                tramo = (int)u.Tramo,
                celda = u.Celda,
                medicamento = u.Medicamento != null ? u.Medicamento.Nombre : "Ocupado"
            }).ToListAsync();

            return Json(occupied);
        }
    }
}

