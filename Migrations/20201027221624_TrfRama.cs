using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class TrfRama : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrfDetallePremioId",
                table: "trf_novedades",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_trf_novedades_TrfDetallePremioId",
                table: "trf_novedades",
                column: "TrfDetallePremioId");

            migrationBuilder.AddForeignKey(
                name: "FK_trf_novedades_trf_detalle_premio_TrfDetallePremioId",
                table: "trf_novedades",
                column: "TrfDetallePremioId",
                principalTable: "trf_detalle_premio",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trf_novedades_trf_detalle_premio_TrfDetallePremioId",
                table: "trf_novedades");

            migrationBuilder.DropIndex(
                name: "IX_trf_novedades_TrfDetallePremioId",
                table: "trf_novedades");

            migrationBuilder.DropColumn(
                name: "TrfDetallePremioId",
                table: "trf_novedades");
        }
    }
}
