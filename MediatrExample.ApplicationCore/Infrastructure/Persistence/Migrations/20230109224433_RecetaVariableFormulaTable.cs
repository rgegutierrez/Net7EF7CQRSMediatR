using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RecetaVariableFormulaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecetaVariableFormula",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaVariableFormulaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Letra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecetaMaquinaPapeleraId = table.Column<int>(type: "int", nullable: true),
                    VariableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaVariableFormula", x => x.RecetaVariableFormulaId);
                    table.ForeignKey(
                        name: "FK_RecetaVariableFormula_RecetaMaquinaPapelera_RecetaMaquinaPapeleraId",
                        column: x => x.RecetaMaquinaPapeleraId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaMaquinaPapelera",
                        principalColumn: "RecetaMaquinaPapeleraId");
                    table.ForeignKey(
                        name: "FK_RecetaVariableFormula_RecetaMaquinaPapelera_VariableId",
                        column: x => x.VariableId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaMaquinaPapelera",
                        principalColumn: "RecetaMaquinaPapeleraId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaVariableFormula_RecetaMaquinaPapeleraId",
                schema: "trzreceta",
                table: "RecetaVariableFormula",
                column: "RecetaMaquinaPapeleraId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaVariableFormula_VariableId",
                schema: "trzreceta",
                table: "RecetaVariableFormula",
                column: "VariableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetaVariableFormula",
                schema: "trzreceta");
        }
    }
}
