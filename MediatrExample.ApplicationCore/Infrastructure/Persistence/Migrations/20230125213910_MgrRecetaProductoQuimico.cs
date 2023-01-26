using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrRecetaProductoQuimico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecetaProductoQuimico",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaProductoQuimicoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false),
                    CodigoSap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Funcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certificacion = table.Column<bool>(type: "bit", nullable: false),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaProductoQuimico", x => x.RecetaProductoQuimicoId);
                    table.ForeignKey(
                        name: "FK_RecetaProductoQuimico_RecetaFabricacion_RecetaFabricacionId",
                        column: x => x.RecetaFabricacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFabricacion",
                        principalColumn: "RecetaFabricacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaProductoQuimico_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaProductoQuimico",
                column: "RecetaFabricacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetaProductoQuimico",
                schema: "trzreceta");
        }
    }
}
