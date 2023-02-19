using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrRecetaIndicadorVacioAjuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecetaTipoIndicadorVacio_LineaProduccion_TipoIndicadorVacioId",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaTipoIndicadorVacio_TipoIndicadorVacio_TipoIndicadorVacioId",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio",
                column: "TipoIndicadorVacioId",
                principalSchema: "trzreceta",
                principalTable: "TipoIndicadorVacio",
                principalColumn: "TipoIndicadorVacioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecetaTipoIndicadorVacio_TipoIndicadorVacio_TipoIndicadorVacioId",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaTipoIndicadorVacio_LineaProduccion_TipoIndicadorVacioId",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio",
                column: "TipoIndicadorVacioId",
                principalSchema: "trzreceta",
                principalTable: "LineaProduccion",
                principalColumn: "LineaProduccionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
