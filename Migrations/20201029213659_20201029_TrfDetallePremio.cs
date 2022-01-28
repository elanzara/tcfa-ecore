using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class _20201029_TrfDetallePremio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "id",
                table: "trf_detalle_premio",
                newName: "IdNovedades");

            migrationBuilder.AddColumn<int>(
                name: "IdTrfNovedades",
                table: "trf_detalle_premio",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_trf_detalle_premio_IdTrfNovedades",
                table: "trf_detalle_premio",
                column: "IdTrfNovedades");

            migrationBuilder.AddForeignKey(
                name: "fk_detalle_premio_novedades",
                table: "trf_detalle_premio",
                column: "IdTrfNovedades",
                principalTable: "trf_novedades",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_detalle_premio_novedades",
                table: "trf_detalle_premio");

            migrationBuilder.DropIndex(
                name: "IX_trf_detalle_premio_IdTrfNovedades",
                table: "trf_detalle_premio");

            migrationBuilder.DropColumn(
                name: "IdTrfNovedades",
                table: "trf_detalle_premio");

            migrationBuilder.RenameColumn(
                name: "IdNovedades",
                table: "trf_detalle_premio",
                newName: "id");

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
    }
}
