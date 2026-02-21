using System.ComponentModel.DataAnnotations;

namespace DispensarioMedicoUnapec.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Guardaremos la contraseña encriptada

        public string Rol { get; set; } = "Usuario"; // Rol por defecto
    }
}