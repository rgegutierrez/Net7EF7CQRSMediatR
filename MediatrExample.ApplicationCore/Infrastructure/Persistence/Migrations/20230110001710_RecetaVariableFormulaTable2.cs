using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RecetaVariableFormulaTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecetaVariableFormula_RecetaMaquinaPapelera_VariableId",
                schema: "trzreceta",
                table: "RecetaVariableFormula");

            migrationBuilder.DropIndex(
                name: "IX_RecetaVariableFormula_VariableId",
                schema: "trzreceta",
                table: "RecetaVariableFormula");

            migrationBuilder.DropColumn(
                name: "VariableId",
                schema: "trzreceta",
                table: "RecetaVariableFormula");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VariableId",
                schema: "trzreceta",
                table: "RecetaVariableFormula",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecetaVariableFormula_VariableId",
                schema: "trzreceta",
                table: "RecetaVariableFormula",
                column: "VariableId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaVariableFormula_RecetaMaquinaPapelera_VariableId",
                schema: "trzreceta",
                table: "RecetaVariableFormula",
                column: "VariableId",
                principalSchema: "trzreceta",
                principalTable: "RecetaMaquinaPapelera",
                principalColumn: "RecetaMaquinaPapeleraId");
        }
    }
}
