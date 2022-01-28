using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations.SecureDb
{
    public partial class _20210625_MapNovedades2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_map_datos:variables",
                table: "map_datos:variables");

            migrationBuilder.RenameTable(
                name: "map_datos:variables",
                newName: "map_datos_variables");

            migrationBuilder.RenameIndex(
                name: "IX_map_datos:variables_id_map_novedades",
                table: "map_datos_variables",
                newName: "IX_map_datos_variables_id_map_novedades");

            migrationBuilder.AddPrimaryKey(
                name: "PK_map_datos_variables",
                table: "map_datos_variables",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_map_datos_variables",
                table: "map_datos_variables");

            migrationBuilder.RenameTable(
                name: "map_datos_variables",
                newName: "map_datos:variables");

            migrationBuilder.RenameIndex(
                name: "IX_map_datos_variables_id_map_novedades",
                table: "map_datos:variables",
                newName: "IX_map_datos:variables_id_map_novedades");

            migrationBuilder.AddPrimaryKey(
                name: "PK_map_datos:variables",
                table: "map_datos:variables",
                column: "id");
        }
    }
}
