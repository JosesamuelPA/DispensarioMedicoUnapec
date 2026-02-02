using System.ComponentModel.DataAnnotations;
namespace DispensarioMedicoUnapec.Models
{
    public enum EstadoFarmaco
    {
        Solido,
        Liquido,
        Semisolido,
        Gaseoso
    }
    public class Tipo_Farmaco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre del Tipo de Farmaco es obligatorio")]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Estado del Farmaco es obligatorio")]
        public EstadoFarmaco EstadoFarmaco { get; set; }

    }
}
