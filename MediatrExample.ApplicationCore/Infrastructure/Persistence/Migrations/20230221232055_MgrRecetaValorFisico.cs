using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MgrRecetaValorFisico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMinimo",
                schema: "trzreceta",
                table: "ValorFisicoPieMaquina",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMaximo",
                schema: "trzreceta",
                table: "ValorFisicoPieMaquina",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "ValorFisicoPieMaquina",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "RecetaMaquinaPapelera",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "MaquinaPapelera",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "RecetaValorFisicoPieMaquina",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaValorFisicoPieMaquinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false),
                    ValorFisicoPieMaquinaId = table.Column<int>(type: "int", nullable: false),
                    NombreVariable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Obligatoria = table.Column<bool>(type: "bit", nullable: false),
                    ValorMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorEst = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaValorFisicoPieMaquina", x => x.RecetaValorFisicoPieMaquinaId);
                    table.ForeignKey(
                        name: "FK_RecetaValorFisicoPieMaquina_RecetaFabricacion_RecetaFabricacionId",
                        column: x => x.RecetaFabricacionId,
                        principalSchema: "trzreceta",
                        principalTable: "RecetaFabricacion",
                        principalColumn: "RecetaFabricacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaValorFisicoPieMaquina_RecetaFabricacionId",
                schema: "trzreceta",
                table: "RecetaValorFisicoPieMaquina",
                column: "RecetaFabricacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetaValorFisicoPieMaquina",
                schema: "trzreceta");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMinimo",
                schema: "trzreceta",
                table: "ValorFisicoPieMaquina",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMaximo",
                schema: "trzreceta",
                table: "ValorFisicoPieMaquina",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "ValorFisicoPieMaquina",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "RecetaMaquinaPapelera",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                schema: "trzreceta",
                table: "MaquinaPapelera",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
