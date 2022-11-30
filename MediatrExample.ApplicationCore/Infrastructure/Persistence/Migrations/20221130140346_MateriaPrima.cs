using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MateriaPrima : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MateriaPrima",
                schema: "trzreceta",
                columns: table => new
                {
                    MateriaPrimaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoSap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<int>(type: "int", nullable: false),
                    ValorMaximo = table.Column<int>(type: "int", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaPrima", x => x.MateriaPrimaId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MateriaPrima",
                schema: "trzreceta");
        }
    }
}
