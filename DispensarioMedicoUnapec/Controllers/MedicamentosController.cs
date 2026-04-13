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
    public class MedicamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Medicamentos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int pageSize = 10;
            int totalItems = await _context.Medicamentos.CountAsync();
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.TiposFarmaco = await _context.Tipo_Farmacos.ToListAsync();
            ViewBag.Marcas = await _context.Marcas.ToListAsync();

            var applicationDbContext = _context.Medicamentos
                .Include(m => m.Tipo_Farmaco)
                .Include(m => m.Marca)
                .Take(pageSize);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Medicamentos/Filter
        public async Task<IActionResult> Filter(string filter_name, string filter_estado, int? filter_tipo, int page = 1)
        {
            int pageSize = 10;
            var medicamentos = _context.Medicamentos
                .Include(m => m.Tipo_Farmaco)
                .Include(m => m.Marca)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter_name))
            {
                medicamentos = medicamentos.Where(m => m.Nombre.Contains(filter_name) || m.Descripcion.Contains(filter_name));
            }
            if (!string.IsNullOrEmpty(filter_estado) && filter_estado != "Todos")
            {
                medicamentos = medicamentos.Where(m => m.Estado == filter_estado);
            }
            if (filter_tipo.HasValue && filter_tipo.Value > 0)
            {
                medicamentos = medicamentos.Where(m => m.Id_Tipo_Farmaco == filter_tipo.Value);
            }

            int totalItems = await medicamentos.CountAsync();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pagedResults = await medicamentos.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return PartialView("_MedicamentosTable", pagedResults);
        }

        // GET: Medicamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos
                .Include(m => m.Tipo_Farmaco)
                .Include(m => m.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicamento == null)
            {
                return NotFound();
            }

            // Load the storage location for this medicamento (there may be multiple)
            var ubicaciones = await _context.Ubicacion_Medicamentos
                .Include(u => u.Estante)
                .Where(u => u.MedicamentoId == id)
                .ToListAsync();

            ViewBag.Ubicaciones = ubicaciones;

            return View(medicamento);
        }

        // GET: Medicamentos/Create
        public IActionResult Create()
        {
            ViewData["Id_Tipo_Farmaco"] = new SelectList(_context.Tipo_Farmacos, "Id", "Nombre");
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre");
            return View();
        }

        // POST: Medicamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Estado,Dosis,Id_Tipo_Farmaco,MarcaId")] Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Tipo_Farmaco"] = new SelectList(_context.Tipo_Farmacos, "Id", "Nombre", medicamento.Id_Tipo_Farmaco);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", medicamento.MarcaId);
            return View(medicamento);
        }

        // GET: Medicamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }
            ViewData["Id_Tipo_Farmaco"] = new SelectList(_context.Tipo_Farmacos, "Id", "Nombre", medicamento.Id_Tipo_Farmaco);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", medicamento.MarcaId);
            return View(medicamento);
        }

        // POST: Medicamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Estado,Dosis,Id_Tipo_Farmaco,MarcaId")] Medicamento medicamento)
        {
            if (id != medicamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamentoExists(medicamento.Id))
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
            ViewData["Id_Tipo_Farmaco"] = new SelectList(_context.Tipo_Farmacos, "Id", "Nombre", medicamento.Id_Tipo_Farmaco);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", medicamento.MarcaId);
            return View(medicamento);
        }

        // GET: Medicamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos
                .Include(m => m.Tipo_Farmaco)
                .Include(m => m.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicamento == null)
            {
                return NotFound();
            }

            return View(medicamento);
        }

        // POST: Medicamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento != null)
            {
                _context.Medicamentos.Remove(medicamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicamentoExists(int id)
        {
            return _context.Medicamentos.Any(e => e.Id == id);
        }
    }
}
