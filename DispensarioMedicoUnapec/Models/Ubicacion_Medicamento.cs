using System.ComponentModel.DataAnnotations;

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
        // Utilizar Letras para Tramo y numeros para Celda A1 = Primer Tramo, Primera Celda
        
        [Required]
        [StringLength(255)]
        public string Estante { get; set; }
        
        [Required]
        public TramoEstante Tramo { get; set; }

        [Required]
        public int Celda { get; set; }

        [Required]
        public EstadoMedicamento EstadoMedicamento { get; set; }

    }
}
