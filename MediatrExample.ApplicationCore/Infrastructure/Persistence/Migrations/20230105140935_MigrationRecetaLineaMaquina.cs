using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationRecetaLineaMaquina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecetaLineaMaquina",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaLineaMaquinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false),
                    LineaProduccionId = table.Column<int>(type: "int", nullable: false),
                    LineaProduccionNombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaLineaMaquina", x => x.RecetaLineaMaquinaId);
                    table.ForeignKey(
                        name: "FK_RecetaLineaMaquina_LineaProduccion_LineaProduccionId",
                        column: x => x.LineaProduccionId,
                        principalSchema: "trzreceta",
                        principalTable: "LineaProduccion",
                        principalColumn: "LineaProduccionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaLineaMaquina_RecetaFabricacion_RecetaFabricacionId",
                        column: x => x.RecetaFabricacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFabricacion",
                        principalColumn: "RecetaFabricacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecetaMaquinaPapelera",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaMaquinaPapeleraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaLineaMaquinaId = table.Column<int>(type: "int", nullable: false),
                    MaquinaPapeleraId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    ModoIngreso = table.Column<bool>(type: "bit", nullable: false),
                    FormulaCalculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaMaquinaPapelera", x => x.RecetaMaquinaPapeleraId);
                    table.ForeignKey(
                        name: "FK_RecetaMaquinaPapelera_MaquinaPapelera_MaquinaPapeleraId",
                        column: x => x.MaquinaPapeleraId,
                        principalSchema: "trzreceta",
                        principalTable: "MaquinaPapelera",
                        principalColumn: "MaquinaPapeleraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaMaquinaPapelera_RecetaLineaMaquina_RecetaLineaMaquinaId",
                        column: x => x.RecetaLineaMaquinaId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaLineaMaquina",
                        principalColumn: "RecetaLineaMaquinaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaLineaMaquina_LineaProduccionId",
                schema: "trzreceta",
                table: "RecetaLineaMaquina",
                column: "LineaProduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaLineaMaquina_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaLineaMaquina",
                column: "RecetaFabricacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaMaquinaPapelera_MaquinaPapeleraId",
                schema: "trzreceta",
                table: "RecetaMaquinaPapelera",
                column: "MaquinaPapeleraId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaMaquinaPapelera_RecetaLineaMaquinaId",
                schema: "trzreceta",
                table: "RecetaMaquinaPapelera",
                column: "RecetaLineaMaquinaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetaMaquinaPapelera",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "RecetaLineaMaquina",
                schema: "trzreceta");
        }
    }
}
