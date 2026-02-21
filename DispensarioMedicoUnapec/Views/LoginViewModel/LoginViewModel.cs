using System.ComponentModel.DataAnnotations;

namespace DispensarioMedicoUnapec.Views.LoginViewModel // Cambia "TuProyecto" por el nombre real de tu proyecto
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}