using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDecimalMaquinaPapelera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMinimo",
                schema: "trzreceta",
                table: "MaquinaPapelera",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMaximo",
                schema: "trzreceta",
                table: "MaquinaPapelera",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ValorMinimo",
                schema: "trzreceta",
                table: "MaquinaPapelera",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "ValorMaximo",
                schema: "trzreceta",
                table: "MaquinaPapelera",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
