using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationRecetaLineaProduccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecetaMateriaPrima_LineaProduccion_LineaProduccionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetaMateriaPrima_RecetaFabricacion_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima");

            migrationBuilder.DropIndex(
                name: "IX_RecetaMateriaPrima_LineaProduccionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima");

            migrationBuilder.DropColumn(
                name: "LineaProduccionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima");

            migrationBuilder.DropColumn(
                name: "LineaProduccionNombre",
                schema: "trzreceta",
                table: "RecetaMateriaPrima");

            migrationBuilder.RenameColumn(
                name: "RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                newName: "RecetaLineaProduccionId");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaMateriaPrima_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                newName: "IX_RecetaMateriaPrima_RecetaLineaProduccionId");

            migrationBuilder.CreateTable(
                name: "RecetaLineaProduccion",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaLineaProduccionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false),
                    LineaProduccionId = table.Column<int>(type: "int", nullable: false),
                    LineaProduccionNombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaLineaProduccion", x => x.RecetaLineaProduccionId);
                    table.ForeignKey(
                        name: "FK_RecetaLineaProduccion_LineaProduccion_LineaProduccionId",
                        column: x => x.LineaProduccionId,
                        principalSchema: "trzreceta",
                        principalTable: "LineaProduccion",
                        principalColumn: "LineaProduccionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaLineaProduccion_RecetaFabricacion_RecetaFabricacionId",
                        column: x => x.RecetaFabricacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFabricacion",
                        principalColumn: "RecetaFabricacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaMateriaPrima_MateriaPrimaId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                column: "MateriaPrimaId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaLineaProduccion_LineaProduccionId",
                schema: "trzreceta",
                table: "RecetaLineaProduccion",
                column: "LineaProduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaLineaProduccion_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaLineaProduccion",
                column: "RecetaFabricacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaMateriaPrima_MateriaPrima_MateriaPrimaId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                column: "MateriaPrimaId",
                principalSchema: "trzreceta",
                principalTable: "MateriaPrima",
                principalColumn: "MateriaPrimaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaMateriaPrima_RecetaLineaProduccion_RecetaLineaProduccionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                column: "RecetaLineaProduccionId",
                principalSchema: "trzreceta",
                principalTable: "RecetaLineaProduccion",
                principalColumn: "RecetaLineaProduccionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecetaMateriaPrima_MateriaPrima_MateriaPrimaId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetaMateriaPrima_RecetaLineaProduccion_RecetaLineaProduccionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima");

            migrationBuilder.DropTable(
                name: "RecetaLineaProduccion",
                schema: "trzreceta");

            migrationBuilder.DropIndex(
                name: "IX_RecetaMateriaPrima_MateriaPrimaId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima");

            migrationBuilder.RenameColumn(
                name: "RecetaLineaProduccionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                newName: "RecetaFabricacionId");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaMateriaPrima_RecetaLineaProduccionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                newName: "IX_RecetaMateriaPrima_RecetaFabricacionId");

            migrationBuilder.AddColumn<int>(
                name: "LineaProduccionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LineaProduccionNombre",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaMateriaPrima_LineaProduccionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                column: "LineaProduccionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaMateriaPrima_LineaProduccion_LineaProduccionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                column: "LineaProduccionId",
                principalSchema: "trzreceta",
                principalTable: "LineaProduccion",
                principalColumn: "LineaProduccionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaMateriaPrima_RecetaFabricacion_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaMateriaPrima",
                column: "RecetaFabricacionId",
                principalSchema: "trzreceta",
                principalTable: "RecetaFabricacion",
                principalColumn: "RecetaFabricacionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
