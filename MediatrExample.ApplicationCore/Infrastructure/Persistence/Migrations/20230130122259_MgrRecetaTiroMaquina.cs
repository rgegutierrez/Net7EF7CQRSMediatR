using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrRecetaTiroMaquina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecetaTiroMaquina",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaTiroMaquinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaTiroMaquina", x => x.RecetaTiroMaquinaId);
                    table.ForeignKey(
                        name: "FK_RecetaTiroMaquina_RecetaFabricacion_RecetaFabricacionId",
                        column: x => x.RecetaFabricacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFabricacion",
                        principalColumn: "RecetaFabricacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaTiroMaquina_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaTiroMaquina",
                column: "RecetaFabricacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetaTiroMaquina",
                schema: "trzreceta");
        }
    }
}
