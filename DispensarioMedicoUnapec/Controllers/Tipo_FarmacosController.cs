using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DispensarioMedicoUnapec.Data;
using DispensarioMedicoUnapec.Models;

namespace DispensarioMedicoUnapec.Controllers
{
    public class Tipo_FarmacosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Tipo_FarmacosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tipo_Farmacos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tipo_Farmacos.ToListAsync());
        }

        // GET: Tipo_Farmacos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_Farmaco = await _context.Tipo_Farmacos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipo_Farmaco == null)
            {
                return NotFound();
            }

            return View(tipo_Farmaco);
        }

        // GET: Tipo_Farmacos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Farmacos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,EstadoFarmaco")] Tipo_Farmaco tipo_Farmaco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipo_Farmaco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipo_Farmaco);
        }

        // GET: Tipo_Farmacos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_Farmaco = await _context.Tipo_Farmacos.FindAsync(id);
            if (tipo_Farmaco == null)
            {
                return NotFound();
            }
            return View(tipo_Farmaco);
        }

        // POST: Tipo_Farmacos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,EstadoFarmaco")] Tipo_Farmaco tipo_Farmaco)
        {
            if (id != tipo_Farmaco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipo_Farmaco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Tipo_FarmacoExists(tipo_Farmaco.Id))
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
            return View(tipo_Farmaco);
        }

        // GET: Tipo_Farmacos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_Farmaco = await _context.Tipo_Farmacos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipo_Farmaco == null)
            {
                return NotFound();
            }

            return View(tipo_Farmaco);
        }

        // POST: Tipo_Farmacos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipo_Farmaco = await _context.Tipo_Farmacos.FindAsync(id);
            if (tipo_Farmaco != null)
            {
                _context.Tipo_Farmacos.Remove(tipo_Farmaco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Tipo_FarmacoExists(int id)
        {
            return _context.Tipo_Farmacos.Any(e => e.Id == id);
        }
    }
}
