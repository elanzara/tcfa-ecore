using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class TrfDetallePremio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trf_detalle_premio",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    premio = table.Column<decimal>(type: "decimal(12, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trf_detalle_premio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trf_rama",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_trf_detalle_premio = table.Column<int>(nullable: false),
                    bonificacion = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    cuotas_sociales = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    derecho_emi_fijo = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    derecho_emision = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    iibb_empresa = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    iibb_percepcion = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    iibb_riesgo = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    iva = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    iva_percepcion = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    iva_rni = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    imp_internos = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    ley_emerg_vial = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    premio = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    prima = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    prima_neta = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    rama = table.Column<int>(nullable: false),
                    recargo_adm = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    recargo_fin = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    recupero_gastos_asoc = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    sellado_empresa = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    sellado_riesgo = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    servicios_sociales = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    tasa_ssn = table.Column<decimal>(type: "decimal(12, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trf_rama", x => x.id);
                    table.ForeignKey(
                        name: "fk_trf_rama",
                        column: x => x.id_trf_detalle_premio,
                        principalTable: "trf_detalle_premio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trf_rama_id_trf_detalle_premio",
                table: "trf_rama",
                column: "id_trf_detalle_premio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trf_rama");

            migrationBuilder.DropTable(
                name: "trf_detalle_premio");
        }
    }
}
