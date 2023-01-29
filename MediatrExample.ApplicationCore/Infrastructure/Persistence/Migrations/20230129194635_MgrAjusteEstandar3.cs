using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrAjusteEstandar3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estandar",
                schema: "trzreceta",
                columns: table => new
                {
                    EstandarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoPapelId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorFisicoPieMaquinaId = table.Column<int>(type: "int", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorPromedio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estandar", x => x.EstandarId);
                    table.ForeignKey(
                        name: "FK_Estandar_ValorFisicoPieMaquina_ValorFisicoPieMaquinaId",
                        column: x => x.ValorFisicoPieMaquinaId,
                        principalSchema: "trzreceta",
                        principalTable: "ValorFisicoPieMaquina",
                        principalColumn: "ValorFisicoPieMaquinaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estandar_ValorFisicoPieMaquinaId",
                schema: "trzreceta",
                table: "Estandar",
                column: "ValorFisicoPieMaquinaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estandar",
                schema: "trzreceta");
        }
    }
}
