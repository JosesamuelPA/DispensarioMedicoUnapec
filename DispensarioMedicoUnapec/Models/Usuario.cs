using System.ComponentModel.DataAnnotations;

namespace DispensarioMedicoUnapec.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Rol { get; set; } = "Usuario";

        // Profile fields
        [StringLength(100)]
        [Display(Name = "Nombre Completo")]
        public string? NombreCompleto { get; set; }

        [StringLength(150)]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string? Email { get; set; }

        [StringLength(20)]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [StringLength(200)]
        [Display(Name = "Departamento / Cargo")]
        public string? Cargo { get; set; }

        // Initials for the avatar when no image is set
        public string Iniciales =>
            string.IsNullOrEmpty(NombreCompleto)
                ? (Username.Length >= 2 ? Username[..2].ToUpper() : Username.ToUpper())
                : string.Concat(NombreCompleto.Split(' ').Where(p => p.Length > 0).Take(2).Select(p => p[0])).ToUpper();
    }
}