using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RecetaMateriaPrima : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecetaMateriaPrima",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaMateriaPrimaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false),
                    LineaProduccionId = table.Column<int>(type: "int", nullable: false),
                    LineaProduccionNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MateriaPrimaId = table.Column<int>(type: "int", nullable: false),
                    CodigoSap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaMateriaPrima", x => x.RecetaMateriaPrimaId);
                    table.ForeignKey(
                        name: "FK_RecetaMateriaPrima_LineaProduccion_LineaProduccionId",
                        column: x => x.LineaProduccionId,
                        principalSchema: "trzreceta",
                        principalTable: "LineaProduccion",
                        principalColumn: "LineaProduccionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaMateriaPrima_RecetaFabricacion_RecetaFabricacionId",
                        column: x => x.RecetaFabricacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFabricacion",
                        principalColumn: "RecetaFabricacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaMateriaPrima_LineaProduccionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                column: "LineaProduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaMateriaPrima_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                column: "RecetaFabricacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetaMateriaPrima",
                schema: "trzreceta");
        }
    }
}
