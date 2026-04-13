using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using DispensarioMedicoUnapec.Data;
using DispensarioMedicoUnapec.Models.ViewModels;

namespace DispensarioMedicoUnapec.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerfilController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<Models.Usuario?> GetCurrentUser()
        {
            var username = User.Identity?.Name;
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }

        // GET: Perfil
        public async Task<IActionResult> Index()
        {
            var usuario = await GetCurrentUser();
            if (usuario == null) return NotFound();

            var vm = new PerfilViewModel
            {
                Id             = usuario.Id,
                Username       = usuario.Username,
                NombreCompleto = usuario.NombreCompleto,
                Email          = usuario.Email,
                Telefono       = usuario.Telefono,
                Cargo          = usuario.Cargo,
                Rol            = usuario.Rol,
                Iniciales      = usuario.Iniciales,
            };

            ViewBag.CambiarContrasena = new CambiarContrasenaViewModel { UsuarioId = usuario.Id };
            return View(vm);
        }

        // POST: Perfil/GuardarPerfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarPerfil(PerfilViewModel vm)
        {
            ModelState.Remove("ContrasenaActual");
            ModelState.Remove("NuevaContrasena");
            ModelState.Remove("ConfirmarContrasena");

            if (!ModelState.IsValid)
            {
                ViewBag.CambiarContrasena = new CambiarContrasenaViewModel { UsuarioId = vm.Id };
                ViewBag.Error = "Por favor corrige los errores del formulario.";
                return View("Index", vm);
            }

            var usuario = await _context.Usuarios.FindAsync(vm.Id);
            if (usuario == null) return NotFound();

            usuario.NombreCompleto = vm.NombreCompleto;
            usuario.Email          = vm.Email;
            usuario.Telefono       = vm.Telefono;
            usuario.Cargo          = vm.Cargo;

            await _context.SaveChangesAsync();

            // Refresh claims so the sidebar shows the updated name immediately
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  usuario.Username),
                new Claim("Rol",            usuario.Rol ?? "Usuario"),
                new Claim("NombreCompleto", usuario.NombreCompleto ?? string.Empty),
            };
            var identity    = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal   = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            TempData["Success"] = "Perfil actualizado correctamente.";
            return RedirectToAction("Index");
        }

        // POST: Perfil/CambiarContrasena
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarContrasena(CambiarContrasenaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorPassword"] = "Por favor completa todos los campos correctamente.";
                return RedirectToAction("Index");
            }

            var usuario = await _context.Usuarios.FindAsync(vm.UsuarioId);
            if (usuario == null) return NotFound();

            if (usuario.PasswordHash != vm.ContrasenaActual)
            {
                TempData["ErrorPassword"] = "La contraseña actual no es correcta.";
                return RedirectToAction("Index");
            }

            usuario.PasswordHash = vm.NuevaContrasena;
            await _context.SaveChangesAsync();
            TempData["SuccessPassword"] = "Contraseña actualizada correctamente.";
            return RedirectToAction("Index");
        }
    }
}
