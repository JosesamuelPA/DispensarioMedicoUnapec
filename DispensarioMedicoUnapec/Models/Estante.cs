using System.ComponentModel.DataAnnotations;

namespace DispensarioMedicoUnapec.Models
{
    public class Estante
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del estante es obligatorio")]
        [StringLength(255)]
        [Display(Name = "Nombre del Estante/Mueble")]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }
    }
}
