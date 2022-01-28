using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class _20201029_TrfSdtComision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdTrfNovedades",
                table: "trf_detalle_premio",
                newName: "id_trf_novedades");

            migrationBuilder.RenameColumn(
                name: "IdNovedades",
                table: "trf_detalle_premio",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_trf_detalle_premio_IdTrfNovedades",
                table: "trf_detalle_premio",
                newName: "IX_trf_detalle_premio_id_trf_novedades");

            migrationBuilder.CreateTable(
                name: "trf_sdtcomision",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_trf_novedades = table.Column<int>(nullable: false),
                    monto = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    nivc = table.Column<int>(nullable: false),
                    nivt = table.Column<int>(nullable: false),
                    porcentaje = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    rama = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trf_sdtcomision", x => x.id);
                    table.ForeignKey(
                        name: "fk_sdtcomision_novedades",
                        column: x => x.id_trf_novedades,
                        principalTable: "trf_novedades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trf_sdtcomision_id_trf_novedades",
                table: "trf_sdtcomision",
                column: "id_trf_novedades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trf_sdtcomision");

            migrationBuilder.RenameColumn(
                name: "id_trf_novedades",
                table: "trf_detalle_premio",
                newName: "IdTrfNovedades");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "trf_detalle_premio",
                newName: "IdNovedades");

            migrationBuilder.RenameIndex(
                name: "IX_trf_detalle_premio_id_trf_novedades",
                table: "trf_detalle_premio",
                newName: "IX_trf_detalle_premio_IdTrfNovedades");
        }
    }
}
