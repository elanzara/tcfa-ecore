using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations.SecureDb
{
    public partial class _20210628_MapNovedades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "motivo",
                table: "map_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "motivo",
                table: "map_novedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
