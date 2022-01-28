using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class AllianzComisiones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "allianz_comisiones_enc",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    archivo = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    usuario = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    fecha_proceso = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allianz_comisiones_enc", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "allianz_comisiones_det",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_allianz_comisiones_enc = table.Column<int>(nullable: false),
                    organizador = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    productor = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    tipo = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    seccion = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    nro_poliza = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    endoso = table.Column<int>(nullable: false),
                    asegurado = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    mda = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    tipo_cambio = table.Column<int>(nullable: false),
                    premio = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    prima = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    comisiones_devengadas = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    comisiones_devengadas_pesos = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    osseg = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    ib_agente = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    ib_riesgo = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    Provincia_riesgo = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    neto_acreditado = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    neto_acreditado_pesos = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    f_pago = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allianz_comisiones_det", x => x.id);
                    table.ForeignKey(
                        name: "fk_allianz_comisiones_det",
                        column: x => x.id_allianz_comisiones_enc,
                        principalTable: "allianz_comisiones_enc",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_allianz_comisiones_det_id_allianz_comisiones_enc",
                table: "allianz_comisiones_det",
                column: "id_allianz_comisiones_enc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "allianz_comisiones_det");

            migrationBuilder.DropTable(
                name: "allianz_comisiones_enc");
        }
    }
}
