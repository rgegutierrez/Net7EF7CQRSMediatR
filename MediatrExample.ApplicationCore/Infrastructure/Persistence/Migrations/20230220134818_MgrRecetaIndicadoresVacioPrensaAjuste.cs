using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrRecetaIndicadoresVacioPrensaAjuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "TipoIndicadorVacio",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "TipoIndicadorPrensa",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorPrensa",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "TipoIndicadorVacio");

            migrationBuilder.DropColumn(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "TipoIndicadorPrensa");

            migrationBuilder.DropColumn(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio");

            migrationBuilder.DropColumn(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorPrensa");
        }
    }
}
