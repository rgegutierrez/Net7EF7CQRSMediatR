using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrRecetaIndicadorVacio2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecetaTipoIndicadorVacio",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaTipoIndicadorVacioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false),
                    TipoIndicadorVacioId = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaTipoIndicadorVacio", x => x.RecetaTipoIndicadorVacioId);
                    table.ForeignKey(
                        name: "FK_RecetaTipoIndicadorVacio_LineaProduccion_TipoIndicadorVacioId",
                        column: x => x.TipoIndicadorVacioId,
                        principalSchema: "trzreceta",
                        principalTable: "LineaProduccion",
                        principalColumn: "LineaProduccionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaTipoIndicadorVacio_RecetaFabricacion_RecetaFabricacionId",
                        column: x => x.RecetaFabricacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFabricacion",
                        principalColumn: "RecetaFabricacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecetaIndicadorVacio",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaIndicadorVacioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaTipoIndicadorVacioId = table.Column<int>(type: "int", nullable: false),
                    IndicadorVacioId = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaIndicadorVacio", x => x.RecetaIndicadorVacioId);
                    table.ForeignKey(
                        name: "FK_RecetaIndicadorVacio_IndicadorVacio_IndicadorVacioId",
                        column: x => x.IndicadorVacioId,
                        principalSchema: "trzreceta",
                        principalTable: "IndicadorVacio",
                        principalColumn: "IndicadorVacioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaIndicadorVacio_RecetaTipoIndicadorVacio_RecetaTipoIndicadorVacioId",
                        column: x => x.RecetaTipoIndicadorVacioId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaTipoIndicadorVacio",
                        principalColumn: "RecetaTipoIndicadorVacioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIndicadorVacio_IndicadorVacioId",
                schema: "trzreceta",
                table: "RecetaIndicadorVacio",
                column: "IndicadorVacioId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIndicadorVacio_RecetaTipoIndicadorVacioId",
                schema: "trzreceta",
                table: "RecetaIndicadorVacio",
                column: "RecetaTipoIndicadorVacioId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaTipoIndicadorVacio_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio",
                column: "RecetaFabricacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaTipoIndicadorVacio_TipoIndicadorVacioId",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio",
                column: "TipoIndicadorVacioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetaIndicadorVacio",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "RecetaTipoIndicadorVacio",
                schema: "trzreceta");
        }
    }
}
