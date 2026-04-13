using System;
using System.ComponentModel.DataAnnotations;

namespace DispensarioMedicoUnapec.Models.visitas
{
    public class Visita
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string Motivo { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
    }
}