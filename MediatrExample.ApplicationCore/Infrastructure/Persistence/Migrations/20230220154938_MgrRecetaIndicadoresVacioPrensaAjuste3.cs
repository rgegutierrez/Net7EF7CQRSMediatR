using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrRecetaIndicadoresVacioPrensaAjuste3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnidadMedidad",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio",
                newName: "UnidadMedida");

            migrationBuilder.RenameColumn(
                name: "UnidadMedidad",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorSecador",
                newName: "UnidadMedida");

            migrationBuilder.RenameColumn(
                name: "UnidadMedidad",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorPrensa",
                newName: "UnidadMedida");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorVacio",
                newName: "UnidadMedidad");

            migrationBuilder.RenameColumn(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorSecador",
                newName: "UnidadMedidad");

            migrationBuilder.RenameColumn(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorPrensa",
                newName: "UnidadMedidad");
        }
    }
}
