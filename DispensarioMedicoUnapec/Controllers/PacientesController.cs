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
    public class PacientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PacientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int pageSize = 10;
            int totalItems = await _context.Pacientes.CountAsync();
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            return View(await _context.Pacientes.Take(pageSize).ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            // Load all visits for this patient, including the attending doctor
            var visitas = await _context.Visitas
                .Include(v => v.Medico)
                .Where(v => v.PacienteId == id)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();

            ViewBag.Visitas = visitas;

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }
        // GET: Pacientes/Filter
        public async Task<IActionResult> Filter(string filter_name, string filter_lastName, string filter_id, string filter_typePatients, string filter_statusPatients, int page = 1)
        {
            int pageSize = 10;
            var pacientes = _context.Pacientes.AsQueryable();

            if (!string.IsNullOrEmpty(filter_name))
            {
                pacientes = pacientes.Where(p => p.Nombre.Contains(filter_name));
            }
            if (!string.IsNullOrEmpty(filter_lastName))
            {
                pacientes = pacientes.Where(p => p.Apellido.Contains(filter_lastName));
            }
            if (!string.IsNullOrEmpty(filter_id))
            {
                pacientes = pacientes.Where(p => p.Cedula.Contains(filter_id));
            }
            if (!string.IsNullOrEmpty(filter_typePatients) && filter_typePatients != "Todos")
            {
                if (Enum.TryParse<TipoPaciente>(filter_typePatients, out var tipo))
                {
                    pacientes = pacientes.Where(p => p.Tipo_Paciente == tipo);
                }
            }
            if (!string.IsNullOrEmpty(filter_statusPatients) && filter_statusPatients != "Todos")
            {
                if (Enum.TryParse<EstadoPaciente>(filter_statusPatients, out var estado))
                {
                    pacientes = pacientes.Where(p => p.Estado_Paciente == estado);
                }
            }

            int totalItems = await pacientes.CountAsync();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pagedResults = await pacientes.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return PartialView("_PacientesTable", pagedResults);
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,FechaNacimiento,Cedula,Telefono,Numero_Carnet,Tipo_Paciente,Estado_Paciente")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,FechaNacimiento,Cedula,Telefono,Numero_Carnet,Tipo_Paciente,Estado_Paciente")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
