using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispensarioMedicoUnapec.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandToMedicamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_MarcaId",
                table: "Medicamentos",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Marcas_MarcaId",
                table: "Medicamentos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Marcas_MarcaId",
                table: "Medicamentos");

            migrationBuilder.DropIndex(
                name: "IX_Medicamentos_MarcaId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Medicamentos");
        }
    }
}
