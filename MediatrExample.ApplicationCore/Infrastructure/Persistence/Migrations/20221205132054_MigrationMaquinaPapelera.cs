using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationMaquinaPapelera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaquinaPapelera",
                schema: "trzreceta",
                columns: table => new
                {
                    MaquinaPapeleraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<int>(type: "int", nullable: false),
                    ValorMaximo = table.Column<int>(type: "int", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    ModoIngreso = table.Column<bool>(type: "bit", nullable: false),
                    FormulaCalculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaquinaPapelera", x => x.MaquinaPapeleraId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaquinaPapelera",
                schema: "trzreceta");
        }
    }
}
