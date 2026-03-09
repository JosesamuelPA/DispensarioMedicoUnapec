using System;
using System.ComponentModel.DataAnnotations;

namespace DispensarioMedicoUnapec.Models
{
    public class Visita
    {
        public int Id { get; set; }

        [Required]
        public string Paciente { get; set; }

        [Required]
        public string Medico { get; set; }

        public string Motivo { get; set; }

        public DateTime Fecha { get; set; }
    }
}