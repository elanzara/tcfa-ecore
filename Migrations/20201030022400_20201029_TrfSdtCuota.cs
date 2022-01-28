using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class _20201029_TrfSdtCuota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trf_sdtcuota",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_trf_novedades = table.Column<int>(nullable: false),
                    estado = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    fecha_cancelada = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    fecha_vto_cuota = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    importe_cuota = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    numero_cuota = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trf_sdtcuota", x => x.id);
                    table.ForeignKey(
                        name: "fk_sdtcuota_novedades",
                        column: x => x.id_trf_novedades,
                        principalTable: "trf_novedades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trf_sdtcuota_id_trf_novedades",
                table: "trf_sdtcuota",
                column: "id_trf_novedades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trf_sdtcuota");
        }
    }
}
