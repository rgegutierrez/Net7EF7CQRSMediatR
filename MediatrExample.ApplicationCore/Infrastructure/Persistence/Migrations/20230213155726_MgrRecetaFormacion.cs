using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrRecetaFormacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecetaFormacion",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaFormacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedidaAngulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RangoAnguloMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RangoAnguloMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnidadMedidaAltura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RangoAlturaMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RangoAlturaMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaFormacion", x => x.RecetaFormacionId);
                    table.ForeignKey(
                        name: "FK_RecetaFormacion_RecetaFabricacion_RecetaFabricacionId",
                        column: x => x.RecetaFabricacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFabricacion",
                        principalColumn: "RecetaFabricacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecetaFormacionValor",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaFormacionValorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFormacionId = table.Column<int>(type: "int", nullable: false),
                    Foil = table.Column<int>(type: "int", nullable: false),
                    ValorAngulo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorAltura = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaFormacionValor", x => x.RecetaFormacionValorId);
                    table.ForeignKey(
                        name: "FK_RecetaFormacionValor_RecetaFormacion_RecetaFormacionId",
                        column: x => x.RecetaFormacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFormacion",
                        principalColumn: "RecetaFormacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaFormacion_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaFormacion",
                column: "RecetaFabricacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaFormacionValor_RecetaFormacionId",
                schema: "trzreceta",
                table: "RecetaFormacionValor",
                column: "RecetaFormacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetaFormacionValor",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "RecetaFormacion",
                schema: "trzreceta");
        }
    }
}
