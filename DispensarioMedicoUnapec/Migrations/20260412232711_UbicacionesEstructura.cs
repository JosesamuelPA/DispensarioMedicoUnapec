using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispensarioMedicoUnapec.Migrations
{
    /// <inheritdoc />
    public partial class UbicacionesEstructura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estante",
                table: "Ubicacion_Medicamentos");

            migrationBuilder.AddColumn<int>(
                name: "EstanteId",
                table: "Ubicacion_Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MedicamentoId",
                table: "Ubicacion_Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Estantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estantes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_Medicamentos_EstanteId",
                table: "Ubicacion_Medicamentos",
                column: "EstanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_Medicamentos_MedicamentoId",
                table: "Ubicacion_Medicamentos",
                column: "MedicamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ubicacion_Medicamentos_Estantes_EstanteId",
                table: "Ubicacion_Medicamentos",
                column: "EstanteId",
                principalTable: "Estantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ubicacion_Medicamentos_Medicamentos_MedicamentoId",
                table: "Ubicacion_Medicamentos",
                column: "MedicamentoId",
                principalTable: "Medicamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ubicacion_Medicamentos_Estantes_EstanteId",
                table: "Ubicacion_Medicamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ubicacion_Medicamentos_Medicamentos_MedicamentoId",
                table: "Ubicacion_Medicamentos");

            migrationBuilder.DropTable(
                name: "Estantes");

            migrationBuilder.DropIndex(
                name: "IX_Ubicacion_Medicamentos_EstanteId",
                table: "Ubicacion_Medicamentos");

            migrationBuilder.DropIndex(
                name: "IX_Ubicacion_Medicamentos_MedicamentoId",
                table: "Ubicacion_Medicamentos");

            migrationBuilder.DropColumn(
                name: "EstanteId",
                table: "Ubicacion_Medicamentos");

            migrationBuilder.DropColumn(
                name: "MedicamentoId",
                table: "Ubicacion_Medicamentos");

            migrationBuilder.AddColumn<string>(
                name: "Estante",
                table: "Ubicacion_Medicamentos",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
