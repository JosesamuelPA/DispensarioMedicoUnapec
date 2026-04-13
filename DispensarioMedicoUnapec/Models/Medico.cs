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
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres")]
        public string Apellido { get; set; }

        // Guardar Solo la Fecha
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "La cédula debe tener exactamente 11 caracteres")]
        public string Cedula { get; set; }
        [Display(Name = "Numero Carnet")]
        [StringLength(50, ErrorMessage = "El carnet no puede exceder los 50 caracteres")]
        public string? NumeroCarnet { get; set; }
        [Display(Name = "Tanda Laboral")]
        public TandaLaboral TandaLaboral { get; set; }
        [StringLength(150, ErrorMessage = "La especialidad no puede exceder los 150 caracteres")]
        public string? Especialidad { get; set; }
        [Display(Name = "Estado Medico")]
        public EstadoMedico EstadoMedico { get; set; }

        public string InfoDisplay
        {
            get { return $"{Nombre} {Apellido} - {Cedula}"; }
        }
    }
}
