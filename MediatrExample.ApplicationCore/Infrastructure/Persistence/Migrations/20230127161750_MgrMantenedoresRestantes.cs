using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrMantenedoresRestantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoIndicadorPrensa",
                schema: "trzreceta",
                columns: table => new
                {
                    TipoIndicadorPrensaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIndicadorPrensa", x => x.TipoIndicadorPrensaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoIndicadorSecador",
                schema: "trzreceta",
                columns: table => new
                {
                    TipoIndicadorSecadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIndicadorSecador", x => x.TipoIndicadorSecadorId);
                });

            migrationBuilder.CreateTable(
                name: "TipoIndicadorVacio",
                schema: "trzreceta",
                columns: table => new
                {
                    TipoIndicadorVacioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIndicadorVacio", x => x.TipoIndicadorVacioId);
                });

            migrationBuilder.CreateTable(
                name: "ValorFisicoPieMaquina",
                schema: "trzreceta",
                columns: table => new
                {
                    ValorFisicoPieMaquinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValorFisicoPieMaquina", x => x.ValorFisicoPieMaquinaId);
                });

            migrationBuilder.CreateTable(
                name: "IndicadorPrensa",
                schema: "trzreceta",
                columns: table => new
                {
                    IndicadorPrensaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIndicadorPrensaId = table.Column<int>(type: "int", nullable: true),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicadorPrensa", x => x.IndicadorPrensaId);
                    table.ForeignKey(
                        name: "FK_IndicadorPrensa_TipoIndicadorPrensa_TipoIndicadorPrensaId",
                        column: x => x.TipoIndicadorPrensaId,
                        principalSchema: "trzreceta",
                        principalTable: "TipoIndicadorPrensa",
                        principalColumn: "TipoIndicadorPrensaId");
                });

            migrationBuilder.CreateTable(
                name: "IndicadorSecador",
                schema: "trzreceta",
                columns: table => new
                {
                    IndicadorSecadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIndicadorSecadorId = table.Column<int>(type: "int", nullable: true),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicadorSecador", x => x.IndicadorSecadorId);
                    table.ForeignKey(
                        name: "FK_IndicadorSecador_TipoIndicadorSecador_TipoIndicadorSecadorId",
                        column: x => x.TipoIndicadorSecadorId,
                        principalSchema: "trzreceta",
                        principalTable: "TipoIndicadorSecador",
                        principalColumn: "TipoIndicadorSecadorId");
                });

            migrationBuilder.CreateTable(
                name: "IndicadorVacio",
                schema: "trzreceta",
                columns: table => new
                {
                    IndicadorVacioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIndicadorVacioId = table.Column<int>(type: "int", nullable: true),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicadorVacio", x => x.IndicadorVacioId);
                    table.ForeignKey(
                        name: "FK_IndicadorVacio_TipoIndicadorVacio_TipoIndicadorVacioId",
                        column: x => x.TipoIndicadorVacioId,
                        principalSchema: "trzreceta",
                        principalTable: "TipoIndicadorVacio",
                        principalColumn: "TipoIndicadorVacioId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndicadorPrensa_TipoIndicadorPrensaId",
                schema: "trzreceta",
                table: "IndicadorPrensa",
                column: "TipoIndicadorPrensaId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicadorSecador_TipoIndicadorSecadorId",
                schema: "trzreceta",
                table: "IndicadorSecador",
                column: "TipoIndicadorSecadorId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicadorVacio_TipoIndicadorVacioId",
                schema: "trzreceta",
                table: "IndicadorVacio",
                column: "TipoIndicadorVacioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estandar",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "IndicadorPrensa",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "IndicadorSecador",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "IndicadorVacio",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "ValorFisicoPieMaquina",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "TipoIndicadorPrensa",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "TipoIndicadorSecador",
                schema: "trzreceta");

            migrationBuilder.DropTable(
                name: "TipoIndicadorVacio",
                schema: "trzreceta");
        }
    }
}
