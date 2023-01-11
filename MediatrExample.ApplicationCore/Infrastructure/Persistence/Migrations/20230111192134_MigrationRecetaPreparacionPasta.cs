using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationRecetaPreparacionPasta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecetaLineaPreparacion",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaLineaPreparacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false),
                    LineaProduccionId = table.Column<int>(type: "int", nullable: false),
                    LineaProduccionNombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaLineaPreparacion", x => x.RecetaLineaPreparacionId);
                    table.ForeignKey(
                        name: "FK_RecetaLineaPreparacion_LineaProduccion_LineaProduccionId",
                        column: x => x.LineaProduccionId,
                        principalSchema: "trzreceta",
                        principalTable: "LineaProduccion",
                        principalColumn: "LineaProduccionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaLineaPreparacion_RecetaFabricacion_RecetaFabricacionId",
                        column: x => x.RecetaFabricacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFabricacion",
                        principalColumn: "RecetaFabricacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecetaPreparacionPasta",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaPreparacionPastaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaLineaPreparacionId = table.Column<int>(type: "int", nullable: false),
                    PreparacionPastaId = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaPreparacionPasta", x => x.RecetaPreparacionPastaId);
                    table.ForeignKey(
                        name: "FK_RecetaPreparacionPasta_PreparacionPasta_PreparacionPastaId",
                        column: x => x.PreparacionPastaId,
                        principalSchema: "trzreceta",
                        principalTable: "PreparacionPasta",
                        principalColumn: "PreparacionPastaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaPreparacionPasta_RecetaLineaPreparacion_RecetaLineaPreparacionId",
                        column: x => x.RecetaLineaPreparacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaLineaPreparacion",
                        principalColumn: "RecetaLineaPreparacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaLineaPreparacion_LineaProduccionId",
                schema: "trzreceta",
                table: "RecetaLineaPreparacion",
                column: "LineaProduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaLineaPreparacion_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaLineaPreparacion",
                column: "RecetaFabricacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaPreparacionPasta_PreparacionPastaId",
                schema: "trzreceta",
                table: "RecetaPreparacionPasta",
                column: "PreparacionPastaId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaPreparacionPasta_RecetaLineaPreparacionId",
                schema: "trzreceta",
                table: "RecetaPreparacionPasta",
                column: "RecetaLineaPreparacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetaPreparacionPasta",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "RecetaLineaPreparacion",
                schema: "trzreceta");
        }
    }
}
