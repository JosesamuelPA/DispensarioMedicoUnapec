using DispensarioMedicoUnapec.Models;
using DispensarioMedicoUnapec.Data;
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

        public IActionResult Index()
        {
            // Obtener las últimas visitas registradas
            var visitas = _context.Visitas
                .Include(v => v.Paciente)
                .Include(v => v.Medico)
                .OrderByDescending(v => v.Fecha)
                .Take(5)
                .ToList();

            ViewBag.VisitasRecientes = visitas;

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