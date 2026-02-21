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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ubicacion_Medicamentos.ToListAsync());
        }

        // GET: Ubicacion_Medicamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacion_Medicamento = await _context.Ubicacion_Medicamentos
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
            return View();
        }

        // POST: Ubicacion_Medicamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estante,Tramo,Celda,EstadoMedicamento")] Ubicacion_Medicamento ubicacion_Medicamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ubicacion_Medicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(ubicacion_Medicamento);
        }

        // POST: Ubicacion_Medicamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estante,Tramo,Celda,EstadoMedicamento")] Ubicacion_Medicamento ubicacion_Medicamento)
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
    }
}
