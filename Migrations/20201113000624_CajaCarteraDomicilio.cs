using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class CajaCarteraDomicilio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "caja_cartera_domicilio",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_caja_cartera_enc = table.Column<int>(nullable: false),
                    direccion = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    localidad = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    codigo_postal = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    codigo_provincia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caja_cartera_domicilio", x => x.id);
                    table.ForeignKey(
                        name: "fk_caja_cartera_domicilio",
                        column: x => x.id_caja_cartera_enc,
                        principalTable: "caja_cartera_det",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_caja_cartera_domicilio_id_caja_cartera_enc",
                table: "caja_cartera_domicilio",
                column: "id_caja_cartera_enc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "caja_cartera_domicilio");
        }
    }
}
