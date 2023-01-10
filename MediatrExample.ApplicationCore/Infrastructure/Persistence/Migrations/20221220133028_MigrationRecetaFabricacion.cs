using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationRecetaFabricacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecetaFabricacion",
                schema: "trzreceta",
                columns: table => new
                {
                    RecetaFabricacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EspecificacionTecnicaId = table.Column<int>(type: "int", nullable: false),
                    CodigoReceta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    AprobacionGerencia = table.Column<bool>(type: "bit", nullable: true),
                    AprobacionJefatura = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InicioVigencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminoVigencia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaFabricacion", x => x.RecetaFabricacionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetaFabricacion",
                schema: "trzreceta");
        }
    }
}
