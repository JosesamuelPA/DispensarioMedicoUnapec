using DispensarioMedicoUnapec.Data;
using DispensarioMedicoUnapec.Models;
using DispensarioMedicoUnapec.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DispensarioMedicoUnapec.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reportes
        public async Task<IActionResult> Index(TipoReporte TipoSeleccionado = TipoReporte.Pacientes)
        {
            var model = new ReporteViewModel { TipoSeleccionado = TipoSeleccionado };

            model.Resultados = TipoSeleccionado switch
            {
                TipoReporte.Pacientes => await _context.Pacientes.ToListAsync(),
                TipoReporte.Medicos => await _context.Medicos.ToListAsync(),
                TipoReporte.Medicamentos => await _context.Medicamentos.Include(m => m.Tipo_Farmaco).ToListAsync(),
                TipoReporte.Visitas => await _context.Visitas
                                        .Include(v => v.Paciente)
                                        .Include(v => v.Medico)
                                        .ToListAsync(),
                TipoReporte.Marcas => await _context.Marcas.ToListAsync(),
                _ => new List<dynamic>()
            };

            return View(model);
        }

        // NUEVO MÉTODO: GET: Reportes/ImprimirExpediente/5
        public async Task<IActionResult> ImprimirExpediente(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscamos al paciente
            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            // Buscamos todas las visitas asociadas a este paciente, incluyendo los datos del Médico
            var historialVisitas = await _context.Visitas
                .Include(v => v.Medico)
                .Where(v => v.PacienteId == id)
                .OrderByDescending(v => v.Fecha) // Las más recientes primero
                .ToListAsync();

            // Usamos ViewBag para pasar el historial a la vista, manteniendo el paciente como modelo principal
            ViewBag.HistorialVisitas = historialVisitas;

            // Retornamos una vista especial diseñada solo para imprimir
            return View(paciente);
        }
    }
}