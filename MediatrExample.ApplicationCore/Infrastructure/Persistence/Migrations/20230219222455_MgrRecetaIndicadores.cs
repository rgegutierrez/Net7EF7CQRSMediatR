using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrRecetaIndicadores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Orden",
                schema: "trzreceta",
                table: "TipoIndicadorVacio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorVacio",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                schema: "trzreceta",
                table: "TipoIndicadorSecador",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorSecador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                schema: "trzreceta",
                table: "TipoIndicadorPrensa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorPrensa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedidad",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RecetaTipoIndicadorPrensa",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaTipoIndicadorPrensaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false),
                    TipoIndicadorPrensaId = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaTipoIndicadorPrensa", x => x.RecetaTipoIndicadorPrensaId);
                    table.ForeignKey(
                        name: "FK_RecetaTipoIndicadorPrensa_RecetaFabricacion_RecetaFabricacionId",
                        column: x => x.RecetaFabricacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFabricacion",
                        principalColumn: "RecetaFabricacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaTipoIndicadorPrensa_TipoIndicadorPrensa_TipoIndicadorPrensaId",
                        column: x => x.TipoIndicadorPrensaId,
                        principalSchema: "trzreceta",
                        principalTable: "TipoIndicadorPrensa",
                        principalColumn: "TipoIndicadorPrensaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecetaTipoIndicadorSecador",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaTipoIndicadorSecadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false),
                    TipoIndicadorSecadorId = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaTipoIndicadorSecador", x => x.RecetaTipoIndicadorSecadorId);
                    table.ForeignKey(
                        name: "FK_RecetaTipoIndicadorSecador_RecetaFabricacion_RecetaFabricacionId",
                        column: x => x.RecetaFabricacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFabricacion",
                        principalColumn: "RecetaFabricacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaTipoIndicadorSecador_TipoIndicadorSecador_TipoIndicadorSecadorId",
                        column: x => x.TipoIndicadorSecadorId,
                        principalSchema: "trzreceta",
                        principalTable: "TipoIndicadorSecador",
                        principalColumn: "TipoIndicadorSecadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecetaIndicadorPrensa",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaIndicadorPrensaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaTipoIndicadorPrensaId = table.Column<int>(type: "int", nullable: false),
                    IndicadorPrensaId = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaIndicadorPrensa", x => x.RecetaIndicadorPrensaId);
                    table.ForeignKey(
                        name: "FK_RecetaIndicadorPrensa_IndicadorPrensa_IndicadorPrensaId",
                        column: x => x.IndicadorPrensaId,
                        principalSchema: "trzreceta",
                        principalTable: "IndicadorPrensa",
                        principalColumn: "IndicadorPrensaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaIndicadorPrensa_RecetaTipoIndicadorPrensa_RecetaTipoIndicadorPrensaId",
                        column: x => x.RecetaTipoIndicadorPrensaId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaTipoIndicadorPrensa",
                        principalColumn: "RecetaTipoIndicadorPrensaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecetaIndicadorSecador",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaIndicadorSecadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaTipoIndicadorSecadorId = table.Column<int>(type: "int", nullable: false),
                    IndicadorSecadorId = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaIndicadorSecador", x => x.RecetaIndicadorSecadorId);
                    table.ForeignKey(
                        name: "FK_RecetaIndicadorSecador_IndicadorSecador_IndicadorSecadorId",
                        column: x => x.IndicadorSecadorId,
                        principalSchema: "trzreceta",
                        principalTable: "IndicadorSecador",
                        principalColumn: "IndicadorSecadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaIndicadorSecador_RecetaTipoIndicadorSecador_RecetaTipoIndicadorSecadorId",
                        column: x => x.RecetaTipoIndicadorSecadorId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaTipoIndicadorSecador",
                        principalColumn: "RecetaTipoIndicadorSecadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIndicadorPrensa_IndicadorPrensaId",
                schema: "trzreceta",
                table: "RecetaIndicadorPrensa",
                column: "IndicadorPrensaId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIndicadorPrensa_RecetaTipoIndicadorPrensaId",
                schema: "trzreceta",
                table: "RecetaIndicadorPrensa",
                column: "RecetaTipoIndicadorPrensaId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIndicadorSecador_IndicadorSecadorId",
                schema: "trzreceta",
                table: "RecetaIndicadorSecador",
                column: "IndicadorSecadorId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIndicadorSecador_RecetaTipoIndicadorSecadorId",
                schema: "trzreceta",
                table: "RecetaIndicadorSecador",
                column: "RecetaTipoIndicadorSecadorId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaTipoIndicadorPrensa_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorPrensa",
                column: "RecetaFabricacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaTipoIndicadorPrensa_TipoIndicadorPrensaId",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorPrensa",
                column: "TipoIndicadorPrensaId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaTipoIndicadorSecador_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorSecador",
                column: "RecetaFabricacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaTipoIndicadorSecador_TipoIndicadorSecadorId",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorSecador",
                column: "TipoIndicadorSecadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetaIndicadorPrensa",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "RecetaIndicadorSecador",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "RecetaTipoIndicadorPrensa",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "RecetaTipoIndicadorSecador",
                schema: "trzreceta");

            migrationBuilder.DropColumn(
                name: "Orden",
                schema: "trzreceta",
                table: "TipoIndicadorVacio");

            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorVacio");

            migrationBuilder.DropColumn(
                name: "Orden",
                schema: "trzreceta",
                table: "TipoIndicadorSecador");

            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorSecador");

            migrationBuilder.DropColumn(
                name: "Orden",
                schema: "trzreceta",
                table: "TipoIndicadorPrensa");

            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorPrensa");

            migrationBuilder.DropColumn(
                name: "Orden",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio");

            migrationBuilder.DropColumn(
                name: "UnidadMedidad",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio");
        }
    }
}
