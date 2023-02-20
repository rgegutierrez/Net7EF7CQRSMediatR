using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrRecetaIndicadoresSecadorAjuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorVacio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorSecador",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "TipoIndicadorSecador",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorPrensa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorSecador",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "TipoIndicadorSecador");

            migrationBuilder.DropColumn(
                name: "MostrarUnidad",
                schema: "trzreceta",
                table: "RecetaTipoIndicadorSecador");

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorVacio",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorSecador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "TipoIndicadorPrensa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
