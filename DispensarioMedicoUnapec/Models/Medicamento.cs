using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispensarioMedicoUnapec.Models
{
    public class Medicamento
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
        [StringLength(50, ErrorMessage = "El estado no puede exceder los 50 caracteres")]
        public string Estado { get; set; } = "Disponible";

        [Required]
        [StringLength(100, ErrorMessage = "La dosis no puede exceder los 100 caracteres")]
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
