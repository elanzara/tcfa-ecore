using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class CajaCarteraCuotas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "caja_cartera_cuotas",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_caja_cartera_enc = table.Column<int>(nullable: false),
                    num_cuota = table.Column<int>(nullable: false),
                    fecha_vto = table.Column<DateTime>(type: "datetime", nullable: false),
                    situacion = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    prima = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    comision = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    premio = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    porc_inflacion = table.Column<decimal>(type: "decimal(12, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caja_cartera_cuotas", x => x.id);
                    table.ForeignKey(
                        name: "fk_caja_cartera_cuotas",
                        column: x => x.id_caja_cartera_enc,
                        principalTable: "caja_cartera_det",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_caja_cartera_cuotas_id_caja_cartera_enc",
                table: "caja_cartera_cuotas",
                column: "id_caja_cartera_enc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "caja_cartera_cuotas");
        }
    }
}
