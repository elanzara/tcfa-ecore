using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class CajaCarteraDet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "caja_cartera_det",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_caja_cartera_enc = table.Column<int>(nullable: false),
                    compania = table.Column<int>(nullable: false),
                    seccion = table.Column<int>(nullable: false),
                    ramo = table.Column<int>(nullable: false),
                    numero = table.Column<decimal>(type: "Decimal(10)", nullable: false),
                    referencia = table.Column<decimal>(type: "Decimal(10)", nullable: false),
                    observacion = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    fecha_vigencia = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_emision = table.Column<DateTime>(type: "datetime", nullable: false),
                    forma_cobro = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    cbu = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    num_end = table.Column<decimal>(type: "Decimal(10)", nullable: false),
                    cod_mon = table.Column<int>(nullable: false),
                    cod_prod = table.Column<int>(nullable: false),
                    poliza_anterior = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    aglutinador = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    solicitud = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    negocio = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caja_cartera_det", x => x.id);
                    table.ForeignKey(
                        name: "fk_caja_cartera_det",
                        column: x => x.id_caja_cartera_enc,
                        principalTable: "caja_cartera_enc",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_caja_cartera_det_id_caja_cartera_enc",
                table: "caja_cartera_det",
                column: "id_caja_cartera_enc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "caja_cartera_det");
        }
    }
}
