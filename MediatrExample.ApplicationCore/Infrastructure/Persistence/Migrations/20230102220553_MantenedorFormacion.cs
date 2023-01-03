using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MantenedorFormacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formacion",
                schema: "trzreceta",
                columns: table => new
                {
                    FormacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedidaAngulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RangoAnguloMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RangoAnguloMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnidadMedidaAltura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RangoAlturaMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RangoAlturaMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formacion", x => x.FormacionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Formacion",
                schema: "trzreceta");
        }
    }
}
