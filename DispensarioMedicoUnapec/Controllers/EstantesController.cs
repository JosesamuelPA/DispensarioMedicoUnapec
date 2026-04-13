using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DispensarioMedicoUnapec.Data;
using DispensarioMedicoUnapec.Models;
using Microsoft.AspNetCore.Authorization;

namespace DispensarioMedicoUnapec.Controllers
{
    [Authorize]
    public class EstantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Estantes/Create
        public IActionResult Create(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Estantes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion")] Estante estante, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estante);
                await _context.SaveChangesAsync();
                
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Ubicacion_Medicamentos");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(estante);
        }
    }
}
