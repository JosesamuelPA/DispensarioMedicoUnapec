using System.ComponentModel.DataAnnotations;

namespace DispensarioMedicoUnapec.Models
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Pais { get; set; }

    }
}
