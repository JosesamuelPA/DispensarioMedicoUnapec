using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispensarioMedicoUnapec.Models
{
    public class Medicamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Estado { get; set; } = "Disponible";

        [Required]
        public string Dosis { get; set; }

        [Display(Name = "Tipo de Farmaco")]
        public int Id_Tipo_Farmaco { get; set; }

        [Display(Name = "Tipo de Farmaco")]
        [ForeignKey("Id_Tipo_Farmaco")]
        public virtual Tipo_Farmaco? Tipo_Farmaco { get; set; }

        [Required(ErrorMessage = "Debe asignar una marca al medicamento")]
        [Display(Name = "Marca")]
        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")]
        public virtual Marca? Marca { get; set; }
    }
}
