using System.ComponentModel.DataAnnotations;


namespace DispensarioMedicoUnapec.Models
{
    public class Paciente
    {
        // Clave Primaria de la Tabla
        [Key]
        public int Id { get; set; }

        // Validaciones del nombre 
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }
        
        // Validaciones del apellido 
        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50)]
        public string Apellido { get; set; }

        // Guardar Solo la Fecha
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(11)]
        public string Cedula { get; set; }

        [Phone]
        public string Telefono { get; set; }

        public string NombreCompleto
        {
            get { return $"{Nombre} {Apellido}";}
        }

    }
}
