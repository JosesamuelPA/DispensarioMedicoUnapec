using Microsoft.EntityFrameworkCore;
using DispensarioMedicoUnapec.Models;

namespace DispensarioMedicoUnapec.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {

        }

        //Tablas
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Tipo_Farmaco> Tipo_Farmacos { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Ubicacion_Medicamento> Ubicacion_Medicamentos { get; set; }
        public DbSet<Medico> Medicos { get; set; }

        // Esta propiedad representa tu tabla en SQL Server
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
