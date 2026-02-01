using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispensarioMedicoUnapec.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionTablaPacientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Numero_Carnet",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tipo_Paciente",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Numero_Carnet",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Tipo_Paciente",
                table: "Pacientes");
        }
    }
}
