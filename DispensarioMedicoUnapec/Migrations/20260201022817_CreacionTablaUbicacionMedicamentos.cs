using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispensarioMedicoUnapec.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablaUbicacionMedicamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ubicacion_Medicamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estante = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Tramo = table.Column<int>(type: "int", nullable: false),
                    Celda = table.Column<int>(type: "int", nullable: false),
                    EstadoMedicamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicacion_Medicamentos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ubicacion_Medicamentos");
        }
    }
}
