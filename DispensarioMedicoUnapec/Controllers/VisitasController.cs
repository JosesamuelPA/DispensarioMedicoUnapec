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
    public class VisitasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Visitas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int pageSize = 10;
            int totalItems = await _context.Visitas.CountAsync();
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            var applicationDbContext = _context.Visitas.Include(v => v.Medico).Include(v => v.Paciente).OrderByDescending(v => v.Fecha).Take(pageSize);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Visitas/Filter
        public async Task<IActionResult> Filter(string filter_paciente, string filter_medico, DateTime? filter_fecha, int page = 1)
        {
            int pageSize = 10;
            var visitas = _context.Visitas.Include(v => v.Medico).Include(v => v.Paciente).AsQueryable();

            if (!string.IsNullOrEmpty(filter_paciente))
            {
                visitas = visitas.Where(v => v.Paciente.Nombre.Contains(filter_paciente) || v.Paciente.Apellido.Contains(filter_paciente) || v.Paciente.Cedula.Contains(filter_paciente));
            }
            if (!string.IsNullOrEmpty(filter_medico))
            {
                visitas = visitas.Where(v => v.Medico.Nombre.Contains(filter_medico) || v.Medico.Apellido.Contains(filter_medico));
            }
            if (filter_fecha.HasValue)
            {
                visitas = visitas.Where(v => v.Fecha.Date == filter_fecha.Value.Date);
            }

            visitas = visitas.OrderByDescending(v => v.Fecha);

            int totalItems = await visitas.CountAsync();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pagedResults = await visitas.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return PartialView("_VisitasTable", pagedResults);
        }

        // GET: Visitas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visita = await _context.Visitas
                .Include(v => v.Medico)
                .Include(v => v.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // GET: Visitas/Create
        public IActionResult Create()
        {
            var medicosActivos = _context.Medicos.Where(m => m.EstadoMedico == EstadoMedico.A).ToList();
            var pacientesActivos = _context.Pacientes.Where(p => p.Estado_Paciente == EstadoPaciente.A).ToList();

            ViewData["MedicoId"] = new SelectList(medicosActivos, "Id", "Nombre");
            ViewData["PacienteId"] = new SelectList(pacientesActivos, "Id", "Nombre");
            return View();
        }

        // POST: Visitas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacienteId,MedicoId,Motivo,Fecha")] Visita visita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var medicosActivos = _context.Medicos.Where(m => m.EstadoMedico == EstadoMedico.A).ToList();
            var pacientesActivos = _context.Pacientes.Where(p => p.Estado_Paciente == EstadoPaciente.A).ToList();

            ViewData["MedicoId"] = new SelectList(medicosActivos, "Id", "Nombre", visita.MedicoId);
            ViewData["PacienteId"] = new SelectList(pacientesActivos, "Id", "Nombre", visita.PacienteId);
            return View(visita);
        }

        // GET: Visitas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visita = await _context.Visitas.FindAsync(id);
            if (visita == null)
            {
                return NotFound();
            }
            var medicosActivos = _context.Medicos.Where(m => m.EstadoMedico == EstadoMedico.A).ToList();
            var pacientesActivos = _context.Pacientes.Where(p => p.Estado_Paciente == EstadoPaciente.A).ToList();

            ViewData["MedicoId"] = new SelectList(medicosActivos, "Id", "Apellido", visita.MedicoId);
            ViewData["PacienteId"] = new SelectList(pacientesActivos, "Id", "Apellido", visita.PacienteId);
            return View(visita);
        }

        // POST: Visitas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PacienteId,MedicoId,Motivo,Fecha")] Visita visita)
        {
            if (id != visita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitaExists(visita.Id))
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
            var medicosActivos = _context.Medicos.Where(m => m.EstadoMedico == EstadoMedico.A).ToList();
            var pacientesActivos = _context.Pacientes.Where(p => p.Estado_Paciente == EstadoPaciente.A).ToList();

            ViewData["MedicoId"] = new SelectList(medicosActivos, "Id", "Apellido", visita.MedicoId);
            ViewData["PacienteId"] = new SelectList(pacientesActivos, "Id", "Apellido", visita.PacienteId);
            return View(visita);
        }

        // GET: Visitas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visita = await _context.Visitas
                .Include(v => v.Medico)
                .Include(v => v.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // POST: Visitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visita = await _context.Visitas.FindAsync(id);
            if (visita != null)
            {
                _context.Visitas.Remove(visita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitaExists(int id)
        {
            return _context.Visitas.Any(e => e.Id == id);
        }
    }
}
