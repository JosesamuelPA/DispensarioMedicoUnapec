using System.ComponentModel.DataAnnotations;


namespace DispensarioMedicoUnapec.Models
{
    public enum TipoPaciente
    {
        Estudiante,
        Empleado,
        Profesor,
        Otros
    }
    public enum EstadoPaciente
    {
        A,
        I
    }
    public class Paciente
    {
        // Clave Primaria de la Tabla
        [Key]
        public int Id { get; set; }

        // Validaciones del nombre 
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }
        
        // Validaciones del apellido 
        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres")]
        public string Apellido { get; set; }

        // Guardar Solo la Fecha
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "La cédula debe tener exactamente 11 caracteres")]
        public string Cedula { get; set; }

        [Phone]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres")]
        public string? Telefono { get; set; }
        [Display(Name ="Numero Carnet")]
        [StringLength(50, ErrorMessage = "El número de carnet no puede exceder los 50 caracteres")]
        public string? Numero_Carnet { get; set; }
        [Display(Name ="Tipo Paciente")]
        public TipoPaciente Tipo_Paciente { get; set; }
        [Display(Name = "Estado")]
        public EstadoPaciente Estado_Paciente { get; set; }
        public string NombreCompleto
        {
            get { return $"{Nombre} {Apellido}";}
        }

        public string InfoDisplay
        {
            get { return $"{Nombre} {Apellido} - {Cedula}"; }
        }

    }
}
