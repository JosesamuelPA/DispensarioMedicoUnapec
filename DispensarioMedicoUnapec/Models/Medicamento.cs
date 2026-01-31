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
        public string Estado { get; set; }

        [Required]
        public string Dosis { get; set; }
        [Display(Name = "Tipo de Farmaco" )]
        public int Id_Tipo_Farmaco { get; set; }

        [ForeignKey("Id_Tipo_Farmaco")]
        public virtual Tipo_Farmaco? Tipo_Farmaco { get; set; }
    }
}
