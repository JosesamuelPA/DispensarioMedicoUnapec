using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispensarioMedicoUnapec.Models
{
    public class Visita
    {

        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; } // Esta es la Clave Foránea (FK)

        [ForeignKey("PacienteId")]
        public virtual Paciente? Paciente { get; set; } // Propiedad de Navegación


        [Required]
        [Display(Name = "Médico")]
        public int MedicoId { get; set; } // Esta es la Clave Foránea (FK)

        [ForeignKey("MedicoId")]
        public virtual Medico? Medico { get; set; } // Propiedad de Navegación

        [Required]
        public string Motivo { get; set; }
        [Required]
        public DateTime Fecha { get; set; }


    }
}
