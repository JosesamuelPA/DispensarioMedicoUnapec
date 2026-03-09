using Microsoft.AspNetCore.Mvc;
using DispensarioMedicoUnapec.Data;
using DispensarioMedicoUnapec.Models;
using System.Linq;

namespace DispensarioMedicoUnapec.Controllers
{
    public class VisitasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTA
        public IActionResult Index()
        {
            var visitas = _context.Visitas.ToList();
            return View(visitas);
        }

        // REGISTRAR VISITA
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Visita visita)
        {
            if (ModelState.IsValid)
            {
                _context.Visitas.Add(visita);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visita);
        }

        // CONSULTA POR CRITERIOS
        public IActionResult Buscar(string paciente, string medico)
        {
            var visitas = _context.Visitas.AsQueryable();

            if (!string.IsNullOrEmpty(paciente))
            {
                visitas = visitas.Where(v => v.Paciente.Contains(paciente));
            }

            if (!string.IsNullOrEmpty(medico))
            {
                visitas = visitas.Where(v => v.Medico.Contains(medico));
            }

            return View("Index", visitas.ToList());
        }
    }
}