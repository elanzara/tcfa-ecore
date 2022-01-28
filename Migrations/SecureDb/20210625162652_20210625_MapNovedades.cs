using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations.SecureDb
{
    public partial class _20210625_MapNovedades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "map_novedades",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    compania = table.Column<double>(nullable: true),
                    sector = table.Column<double>(nullable: true),
                    ramo = table.Column<double>(nullable: true),
                    poliza = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    productor = table.Column<double>(nullable: true),
                    endoso = table.Column<double>(nullable: true),
                    fechaEmiSpto = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    fechaEfecPol = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    fechaVctoPol = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    fechaEfecSpto = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    fechaVctoSpto = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    codEndoso = table.Column<double>(nullable: true),
                    subEndoso = table.Column<double>(nullable: true),
                    motivo = table.Column<double>(nullable: true),
                    tipoDocumentoTom = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
                    codigoDocumentoTom = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    planPago = table.Column<double>(nullable: true),
                    polizaAnterior = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    polizaMadre = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    polizaSiguiente = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    facturacion = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    fechaRenov = table.Column<string>(unicode: false, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map_novedades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "map_asegurados",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_map_novedades = table.Column<int>(nullable: false),
                    poliza = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    endoso = table.Column<double>(nullable: true),
                    riesgo = table.Column<double>(nullable: true),
                    tipoBenef = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    tipoDoc = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
                    codDoc = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    asegurado = table.Column<string>(unicode: false, maxLength: 120, nullable: true),
                    domicilio = table.Column<string>(unicode: false, maxLength: 120, nullable: true),
                    localidad = table.Column<double>(nullable: true),
                    postal = table.Column<double>(nullable: true),
                    provincia = table.Column<double>(nullable: true),
                    telefonoPais = table.Column<double>(nullable: true),
                    telefonoZona = table.Column<double>(nullable: true),
                    telefono = table.Column<double>(nullable: true),
                    domicilioCom = table.Column<string>(unicode: false, maxLength: 120, nullable: true),
                    localidadCom = table.Column<double>(nullable: true),
                    postalCom = table.Column<double>(nullable: true),
                    provinciaCom = table.Column<double>(nullable: true),
                    nacimiento = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    iva = table.Column<double>(nullable: true),
                    mcaSexo = table.Column<string>(unicode: false, maxLength: 1, nullable: true),
                    nomina = table.Column<string>(unicode: false, maxLength: 1, nullable: true),
                    baja = table.Column<string>(unicode: false, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map_asegurados", x => x.id);
                    table.ForeignKey(
                        name: "fk_asegurados_novedades",
                        column: x => x.id_map_novedades,
                        principalTable: "map_novedades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "map_coberturas",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_map_novedades = table.Column<int>(nullable: false),
                    poliza = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    endoso = table.Column<double>(nullable: true),
                    riesgo = table.Column<double>(nullable: true),
                    secu = table.Column<double>(nullable: true),
                    cobertura = table.Column<double>(nullable: true),
                    sumaAseg = table.Column<decimal>(type: "decimal(8, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map_coberturas", x => x.id);
                    table.ForeignKey(
                        name: "fk_coberturas_novedades",
                        column: x => x.id_map_novedades,
                        principalTable: "map_novedades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "map_cuotas",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_map_novedades = table.Column<int>(nullable: false),
                    poliza = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    endoso = table.Column<double>(nullable: true),
                    numeroRecibo = table.Column<double>(nullable: true),
                    convenio = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    vctoRecibo = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    fecCobro = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    agrpImpositivo = table.Column<double>(nullable: true),
                    medioPago = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    moneda = table.Column<double>(nullable: true),
                    importe = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    cobroAnticipado = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    impComisiones = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    situacionRecibo = table.Column<string>(unicode: false, maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map_cuotas", x => x.id);
                    table.ForeignKey(
                        name: "fk_cuotas_novedades",
                        column: x => x.id_map_novedades,
                        principalTable: "map_novedades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "map_datos:variables",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_map_novedades = table.Column<int>(nullable: false),
                    poliza = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    endoso = table.Column<double>(nullable: true),
                    riesgo = table.Column<double>(nullable: true),
                    campo = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    valor = table.Column<string>(unicode: false, maxLength: 80, nullable: true),
                    descripcion = table.Column<string>(unicode: false, maxLength: 80, nullable: true),
                    nivel = table.Column<double>(nullable: true),
                    secu = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map_datos:variables", x => x.id);
                    table.ForeignKey(
                        name: "fk_datos_variables_novedades",
                        column: x => x.id_map_novedades,
                        principalTable: "map_novedades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "map_datos_riesgo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_map_novedades = table.Column<int>(nullable: false),
                    poliza = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    endoso = table.Column<double>(nullable: true),
                    riesgo = table.Column<double>(nullable: true),
                    nombreRiesgo = table.Column<string>(unicode: false, maxLength: 80, nullable: true),
                    vigencia = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    vencimiento = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    baja = table.Column<string>(unicode: false, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map_datos_riesgo", x => x.id);
                    table.ForeignKey(
                        name: "fk_datos_riesgo_novedades",
                        column: x => x.id_map_novedades,
                        principalTable: "map_novedades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "map_impuestos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_map_novedades = table.Column<int>(nullable: false),
                    poliza = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    endoso = table.Column<double>(nullable: true),
                    primaComisionable = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    primaNoComisionable = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    derEmis = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    recAdmin = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    recFinan = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    bonificaciones = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    bonifAdic = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    otrosImptos = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    servSociales = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    imptosInternos = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    ingBrutos = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    premio = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    porComision = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map_impuestos", x => x.id);
                    table.ForeignKey(
                        name: "fk_impuestos_novedades",
                        column: x => x.id_map_novedades,
                        principalTable: "map_novedades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_map_asegurados_id_map_novedades",
                table: "map_asegurados",
                column: "id_map_novedades");

            migrationBuilder.CreateIndex(
                name: "IX_map_coberturas_id_map_novedades",
                table: "map_coberturas",
                column: "id_map_novedades");

            migrationBuilder.CreateIndex(
                name: "IX_map_cuotas_id_map_novedades",
                table: "map_cuotas",
                column: "id_map_novedades");

            migrationBuilder.CreateIndex(
                name: "IX_map_datos:variables_id_map_novedades",
                table: "map_datos:variables",
                column: "id_map_novedades");

            migrationBuilder.CreateIndex(
                name: "IX_map_datos_riesgo_id_map_novedades",
                table: "map_datos_riesgo",
                column: "id_map_novedades");

            migrationBuilder.CreateIndex(
                name: "IX_map_impuestos_id_map_novedades",
                table: "map_impuestos",
                column: "id_map_novedades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "map_asegurados");

            migrationBuilder.DropTable(
                name: "map_coberturas");

            migrationBuilder.DropTable(
                name: "map_cuotas");

            migrationBuilder.DropTable(
                name: "map_datos:variables");

            migrationBuilder.DropTable(
                name: "map_datos_riesgo");

            migrationBuilder.DropTable(
                name: "map_impuestos");

            migrationBuilder.DropTable(
                name: "map_novedades");
        }
    }
}
