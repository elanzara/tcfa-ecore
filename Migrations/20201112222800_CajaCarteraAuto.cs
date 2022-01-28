using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class CajaCarteraAuto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "caja_cartera_auto",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_caja_cartera_enc = table.Column<int>(nullable: false),
                    patente = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Marca = table.Column<int>(nullable: false),
                    desc_marca = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    modelo = table.Column<int>(nullable: false),
                    suma_asegurada = table.Column<decimal>(type: "decimal(10)", nullable: false),
                    tipo_vehiculo = table.Column<int>(nullable: false),
                    uso_vehiculo = table.Column<int>(nullable: false),
                    clase_vehiculo = table.Column<int>(nullable: false),
                    motor = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    chasis = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    mca_cero_km = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    cod_infoauto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caja_cartera_auto", x => x.id);
                    table.ForeignKey(
                        name: "fk_caja_cartera_auto",
                        column: x => x.id_caja_cartera_enc,
                        principalTable: "caja_cartera_det",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_caja_cartera_auto_id_caja_cartera_enc",
                table: "caja_cartera_auto",
                column: "id_caja_cartera_enc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "caja_cartera_auto");
        }
    }
}
