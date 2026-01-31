using System.ComponentModel.DataAnnotations;
namespace DispensarioMedicoUnapec.Models
{
    public class Tipo_Farmaco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre del Tipo de Farmaco es obligatorio")]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Estado del Farmaco es obligatorio")]
        public string Estado { get; set; }

    }
}
