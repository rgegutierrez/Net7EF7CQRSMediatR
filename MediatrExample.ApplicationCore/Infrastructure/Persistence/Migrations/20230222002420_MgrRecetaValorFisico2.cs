using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrRecetaValorFisico2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RecetaValorFisicoPieMaquina_ValorFisicoPieMaquinaId",
                schema: "trzreceta",
                table: "RecetaValorFisicoPieMaquina",
                column: "ValorFisicoPieMaquinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaValorFisicoPieMaquina_ValorFisicoPieMaquina_ValorFisicoPieMaquinaId",
                schema: "trzreceta",
                table: "RecetaValorFisicoPieMaquina",
                column: "ValorFisicoPieMaquinaId",
                principalSchema: "trzreceta",
                principalTable: "ValorFisicoPieMaquina",
                principalColumn: "ValorFisicoPieMaquinaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecetaValorFisicoPieMaquina_ValorFisicoPieMaquina_ValorFisicoPieMaquinaId",
                schema: "trzreceta",
                table: "RecetaValorFisicoPieMaquina");

            migrationBuilder.DropIndex(
                name: "IX_RecetaValorFisicoPieMaquina_ValorFisicoPieMaquinaId",
                schema: "trzreceta",
                table: "RecetaValorFisicoPieMaquina");
        }
    }
}
