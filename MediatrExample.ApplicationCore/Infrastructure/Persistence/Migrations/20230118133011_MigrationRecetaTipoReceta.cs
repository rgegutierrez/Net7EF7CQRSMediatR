using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationRecetaTipoReceta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoRecetaId",
                schema: "trzreceta",
                table: "RecetaFabricacion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecetaFabricacion_TipoRecetaId",
                schema: "trzreceta",
                table: "RecetaFabricacion",
                column: "TipoRecetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaFabricacion_TipoReceta_TipoRecetaId",
                schema: "trzreceta",
                table: "RecetaFabricacion",
                column: "TipoRecetaId",
                principalSchema: "trzreceta",
                principalTable: "TipoReceta",
                principalColumn: "TipoRecetaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecetaFabricacion_TipoReceta_TipoRecetaId",
                schema: "trzreceta",
                table: "RecetaFabricacion");

            migrationBuilder.DropIndex(
                name: "IX_RecetaFabricacion_TipoRecetaId",
                schema: "trzreceta",
                table: "RecetaFabricacion");

            migrationBuilder.DropColumn(
                name: "TipoRecetaId",
                schema: "trzreceta",
                table: "RecetaFabricacion");
        }
    }
}
