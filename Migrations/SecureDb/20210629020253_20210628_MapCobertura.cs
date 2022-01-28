using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations.SecureDb
{
    public partial class _20210628_MapCobertura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "sumaAseg",
                table: "map_coberturas",
                type: "decimal(12, 2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8, 2)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "sumaAseg",
                table: "map_coberturas",
                type: "decimal(8, 2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12, 2)",
                oldNullable: true);
        }
    }
}
