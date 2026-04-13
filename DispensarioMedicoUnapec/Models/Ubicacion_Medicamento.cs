using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispensarioMedicoUnapec.Models
{
    public enum TramoEstante
    {
        A,
        B,
        C,
        D,
        E,
        F
    }
    public enum EstadoMedicamento
    {
        A,
        I
    }

    public class Ubicacion_Medicamento
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe asignar esta ubicación a un Estante")]
        [Display(Name = "Estante Asignado")]
        public int EstanteId { get; set; }
        
        [ForeignKey("EstanteId")]
        public virtual Estante? Estante { get; set; }

        [Required(ErrorMessage = "Debe seleccionar qué medicamento se guardará aquí")]
        [Display(Name = "Medicamento Almacenado")]
        public int MedicamentoId { get; set; }

        [ForeignKey("MedicamentoId")]
        public virtual Medicamento? Medicamento { get; set; }

        [Required]
        public TramoEstante Tramo { get; set; }

        [Required]
        public int Celda { get; set; }

        [Required]
        [Display(Name ="Estado Medicamento")]
        public EstadoMedicamento EstadoMedicamento { get; set; }

    }
}
