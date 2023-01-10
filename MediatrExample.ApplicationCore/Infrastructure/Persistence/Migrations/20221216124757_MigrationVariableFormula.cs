using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationVariableFormula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VariableFormula",
                schema: "trzreceta",
                columns: table => new
                {
                    VariableFormulaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Letra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaquinaPapeleraId = table.Column<int>(type: "int", nullable: true),
                    VariableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableFormula", x => x.VariableFormulaId);
                    table.ForeignKey(
                        name: "FK_VariableFormula_MaquinaPapelera_MaquinaPapeleraId",
                        column: x => x.MaquinaPapeleraId,
                        principalSchema: "trzreceta",
                        principalTable: "MaquinaPapelera",
                        principalColumn: "MaquinaPapeleraId");
                    table.ForeignKey(
                        name: "FK_VariableFormula_MaquinaPapelera_VariableId",
                        column: x => x.VariableId,
                        principalSchema: "trzreceta",
                        principalTable: "MaquinaPapelera",
                        principalColumn: "MaquinaPapeleraId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariableFormula_MaquinaPapeleraId",
                schema: "trzreceta",
                table: "VariableFormula",
                column: "MaquinaPapeleraId");

            migrationBuilder.CreateIndex(
                name: "IX_VariableFormula_VariableId",
                schema: "trzreceta",
                table: "VariableFormula",
                column: "VariableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariableFormula",
                schema: "trzreceta");
        }
    }
}
