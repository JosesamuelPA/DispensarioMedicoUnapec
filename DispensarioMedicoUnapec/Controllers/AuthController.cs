using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using DispensarioMedicoUnapec.Views.LoginViewModel; // Asegúrate de usar tu namespace real
using DispensarioMedicoUnapec.Data;
using DispensarioMedicoUnapec.Models;
using DispensarioMedicoUnapec.Views;

namespace DispensarioMedicoUnapec.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inyectamos la base de datos en el constructor
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Esto muestra la pantalla de Login (GET)
        [HttpGet]
        public IActionResult Login()
        {
            // Si el usuario ya inició sesión, lo mandamos al inicio
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // 2. Esto procesa el formulario cuando le dan al botón (POST)
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ==========================================================
                // LÓGICA DE BASE DE DATOS (SQL SERVER CON EF CORE)
                // ==========================================================

                // 1. Buscamos en la base de datos un registro que coincida 
                // con el usuario y la contraseña ingresados.
                var usuarioDb = _context.Usuarios
                    .FirstOrDefault(u => u.Username == model.Username && u.PasswordHash == model.Password);

                // 2. Verificamos si encontramos al usuario
                if (usuarioDb != null)
                {
                    // ¡Éxito! El usuario existe y la contraseña es correcta.
                    // Creamos la "credencial" extrayendo los datos reales de la base de datos.
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuarioDb.Username),
                        new Claim("Rol", usuarioDb.Rol ?? "Usuario") // Tomamos el rol de tu tabla
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Iniciamos la sesión creando la cookie
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    // Lo enviamos a la página principal
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Si usuarioDb es null, significa que el usuario no existe o la clave está mal.
                    // Mostramos un error genérico por seguridad (para que no sepan qué falló exactamente).
                    ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                }
            }

            // Si hay errores de validación (ej. campos vacíos), regresamos la misma vista.
            return View(model);
        }

        // Mostrar la pantalla de Registro (GET)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // 3. Para cerrar sesión
        
        [HttpPost]
        [ValidateAntiForgeryToken] // Protege contra ataques CSRF
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Verificar si el usuario ya existe
                bool userExists = _context.Usuarios.Any(u => u.Username == model.Username);
                if (userExists)
                {
                    ModelState.AddModelError("Username", "Este nombre de usuario ya está en uso.");
                    return View(model);
                }

                // 2. Crear el nuevo usuario (Para producción, DEBES encriptar la contraseña. 
                // Aquí la guardamos directo solo para probar que EF Core funciona)
                var nuevoUsuario = new Usuario
                {
                    Username = model.Username,
                    PasswordHash = model.Password // Ideal usar BCrypt.HashPassword(model.Password)
                };

                // 3. Guardar en la base de datos
                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                // 4. Redirigir al login
                TempData["SuccessMessage"] = "Cuenta creada exitosamente. Por favor, inicia sesión.";
                return RedirectToAction("Login", "Auth");
            }

            return View(model);
        }
    }
}