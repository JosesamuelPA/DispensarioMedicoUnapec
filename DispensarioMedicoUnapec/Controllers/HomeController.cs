using DispensarioMedicoUnapec.Data;
using DispensarioMedicoUnapec.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DispensarioMedicoUnapec.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Real counts
            ViewBag.TotalPacientes   = await _context.Pacientes.CountAsync();
            ViewBag.TotalMedicos     = await _context.Medicos.CountAsync();
            ViewBag.TotalMedicamentos = await _context.Medicamentos.CountAsync();
            ViewBag.TotalVisitasHoy  = await _context.Visitas
                                            .CountAsync(v => v.Fecha.Date == DateTime.Today);

            // Last 5 patients who had a visit, ordered by most recent date
            ViewBag.UltimasVisitas = await _context.Visitas
                .Include(v => v.Paciente)
                .Include(v => v.Medico)
                .OrderByDescending(v => v.Fecha)
                .Take(5)
                .ToListAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}