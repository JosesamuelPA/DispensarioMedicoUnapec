using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DispensarioMedicoUnapec.Models
{
    public enum TandaLaboral
    {
        Matutina,
        Vespertina,
        Nocturna
    }
    public enum EstadoMedico
    {
        A,
        I
    }
    public class Medico
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(255)]
        public string Apellido { get; set; }

        // Guardar Solo la Fecha
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(11)]
        public string Cedula { get; set; }
        [Display(Name = "Numero Carnet")]
        public string NumeroCarnet { get; set; }
        [Display(Name = "Tanda Laboral")]
        public TandaLaboral TandaLaboral { get; set; }
        public string Especialidad { get; set; }
        [Display(Name = "Estado Medico")]
        public EstadoMedico EstadoMedico { get; set; }
    }
}
