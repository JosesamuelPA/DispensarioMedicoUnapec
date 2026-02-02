using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispensarioMedicoUnapec.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionTablaTipoFarmaco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Tipo_Farmacos");

            migrationBuilder.AddColumn<int>(
                name: "EstadoFarmaco",
                table: "Tipo_Farmacos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoFarmaco",
                table: "Tipo_Farmacos");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Tipo_Farmacos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
