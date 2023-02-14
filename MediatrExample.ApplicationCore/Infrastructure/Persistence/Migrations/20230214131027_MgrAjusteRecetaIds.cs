using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrAjusteRecetaIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TiroMaquinaId",
                schema: "trzreceta",
                table: "RecetaTiroMaquina",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductoQuimicoId",
                schema: "trzreceta",
                table: "RecetaProductoQuimico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormacionId",
                schema: "trzreceta",
                table: "RecetaFormacion",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TiroMaquinaId",
                schema: "trzreceta",
                table: "RecetaTiroMaquina");

            migrationBuilder.DropColumn(
                name: "ProductoQuimicoId",
                schema: "trzreceta",
                table: "RecetaProductoQuimico");

            migrationBuilder.DropColumn(
                name: "FormacionId",
                schema: "trzreceta",
                table: "RecetaFormacion");
        }
    }
}
