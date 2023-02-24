using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrFactorIndicadores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Factor",
                schema: "trzreceta",
                table: "TipoIndicadorVacio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Factor",
                schema: "trzreceta",
                table: "TipoIndicadorSecador",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Factor",
                schema: "trzreceta",
                table: "TipoIndicadorPrensa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Factor",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Factor",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorSecador",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Factor",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorPrensa",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Factor",
                schema: "trzreceta",
                table: "TipoIndicadorVacio");

            migrationBuilder.DropColumn(
                name: "Factor",
                schema: "trzreceta",
                table: "TipoIndicadorSecador");

            migrationBuilder.DropColumn(
                name: "Factor",
                schema: "trzreceta",
                table: "TipoIndicadorPrensa");

            migrationBuilder.DropColumn(
                name: "Factor",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio");

            migrationBuilder.DropColumn(
                name: "Factor",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorSecador");

            migrationBuilder.DropColumn(
                name: "Factor",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorPrensa");
        }
    }
}
