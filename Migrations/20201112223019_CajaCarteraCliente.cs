using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class CajaCarteraCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "caja_cartera_cliente",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_caja_cartera_enc = table.Column<int>(nullable: false),
                    tipo_documento = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    nro_documento = table.Column<decimal>(type: "decimal(10)", nullable: false),
                    apellido = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    nombre = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    fecha_nacimiento = table.Column<DateTime>(type: "datetime", nullable: false),
                    cod_iva = table.Column<int>(nullable: false),
                    sexo = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    est_civil = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caja_cartera_cliente", x => x.id);
                    table.ForeignKey(
                        name: "fk_caja_cartera_cliente",
                        column: x => x.id_caja_cartera_enc,
                        principalTable: "caja_cartera_det",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_caja_cartera_cliente_id_caja_cartera_enc",
                table: "caja_cartera_cliente",
                column: "id_caja_cartera_enc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "caja_cartera_cliente");
        }
    }
}
