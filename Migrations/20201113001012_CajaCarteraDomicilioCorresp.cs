using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class CajaCarteraDomicilioCorresp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "caja_cartera_domicilio_corresp",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_caja_cartera_enc = table.Column<int>(nullable: false),
                    direccion = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    localidad = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    codigo_postal = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    codigo_provincia = table.Column<int>(nullable: false),
                    telefono = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caja_cartera_domicilio_corresp", x => x.id);
                    table.ForeignKey(
                        name: "fk_caja_cartera_domicilio_corresp",
                        column: x => x.id_caja_cartera_enc,
                        principalTable: "caja_cartera_det",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_caja_cartera_domicilio_corresp_id_caja_cartera_enc",
                table: "caja_cartera_domicilio_corresp",
                column: "id_caja_cartera_enc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "caja_cartera_domicilio_corresp");
        }
    }
}
