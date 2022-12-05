using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MaquinaPapeleraLineaProduccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LineaProduccion",
                schema: "trzreceta",
                table: "MaquinaPapelera",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineaProduccion",
                schema: "trzreceta",
                table: "MaquinaPapelera");
        }
    }
}
