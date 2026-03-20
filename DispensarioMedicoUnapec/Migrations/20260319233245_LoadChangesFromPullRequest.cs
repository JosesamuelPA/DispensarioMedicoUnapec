using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispensarioMedicoUnapec.Migrations
{
    /// <inheritdoc />
    public partial class LoadChangesFromPullRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Medico",
                table: "Visitas");

            migrationBuilder.DropColumn(
                name: "Paciente",
                table: "Visitas");

            migrationBuilder.AddColumn<int>(
                name: "MedicoId",
                table: "Visitas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Visitas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_MedicoId",
                table: "Visitas",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_PacienteId",
                table: "Visitas",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Medicos_MedicoId",
                table: "Visitas",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Pacientes_PacienteId",
                table: "Visitas",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Medicos_MedicoId",
                table: "Visitas");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Pacientes_PacienteId",
                table: "Visitas");

            migrationBuilder.DropIndex(
                name: "IX_Visitas_MedicoId",
                table: "Visitas");

            migrationBuilder.DropIndex(
                name: "IX_Visitas_PacienteId",
                table: "Visitas");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "Visitas");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Visitas");

            migrationBuilder.AddColumn<string>(
                name: "Medico",
                table: "Visitas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Paciente",
                table: "Visitas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
