using System.ComponentModel.DataAnnotations;

namespace DispensarioMedicoUnapec.Models
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        public string Descripcion { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El país no puede exceder los 100 caracteres")]
        public string Pais { get; set; }

    }
}
