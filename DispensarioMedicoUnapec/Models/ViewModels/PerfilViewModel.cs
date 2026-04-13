using System.ComponentModel.DataAnnotations;

namespace DispensarioMedicoUnapec.Models.ViewModels
{
    public class PerfilViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Nombre Completo")]
        [StringLength(100)]
        public string? NombreCompleto { get; set; }

        [Display(Name = "Correo Electrónico")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [StringLength(150)]
        public string? Email { get; set; }

        [Display(Name = "Teléfono")]
        [StringLength(20)]
        public string? Telefono { get; set; }

        [Display(Name = "Cargo / Departamento")]
        [StringLength(200)]
        public string? Cargo { get; set; }

        public string? Rol { get; set; }
        public string? Iniciales { get; set; }
    }

    public class CambiarContrasenaViewModel
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "La contraseña actual es requerida")]
        [Display(Name = "Contraseña Actual")]
        public string ContrasenaActual { get; set; } = string.Empty;

        [Required(ErrorMessage = "La nueva contraseña es requerida")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Display(Name = "Nueva Contraseña")]
        public string NuevaContrasena { get; set; } = string.Empty;

        [Compare("NuevaContrasena", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmarContrasena { get; set; } = string.Empty;
    }
}
