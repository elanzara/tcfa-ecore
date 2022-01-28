using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class AllianzCartera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "allianz_cartera_enc",
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
                    table.PrimaryKey("PK_allianz_cartera_enc", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "allianz_cartera_det",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_allianz_cartera_enc = table.Column<int>(nullable: false),
                    productor = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    organizador = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    seccion = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    poliza = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    endoso = table.Column<int>(nullable: false),
                    clase_endoso = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    nombre_del_asegurado = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    fec_emision = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_desde_vigencia = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_hasta_vigencia = table.Column<DateTime>(type: "datetime", nullable: false),
                    estado = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    moneda = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    total_prima = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    comision_org = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    comision_prod = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    total_premio = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    total_pagado = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    saldo = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    cant_siniestros = table.Column<int>(nullable: false),
                    cant_denuncias = table.Column<int>(nullable: false),
                    tipo_de_documento = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    numero_de_documento = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    cant_cuotas = table.Column<int>(nullable: false),
                    forma_de_cobro = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    tipo_operacion = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    derecho_emision = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    gastos_financ = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    iva = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    sellos = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    gastos_adm = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    suma_asegurada = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    valor_de_referencia = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    tipo_poliza = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    cuatrimestre = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    estado_solicitud = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    fecha_desp_imp = table.Column<DateTime>(type: "datetime", nullable: false),
                    propuesta = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    linea = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    poliza_renovada = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    cant_cuotas2 = table.Column<int>(nullable: false),
                    porc_1er_cuota = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    venc_1era_cuota = table.Column<int>(nullable: false),
                    plan_pago = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    nro_interno = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    fecha_vto_poliza = table.Column<DateTime>(type: "datetime", nullable: false),
                    patente = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    marca = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    modelo = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    motor = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    chasis = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    uso = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    cobertura = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    suma_asegurada2 = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    valor_de_referencia2 = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    item = table.Column<int>(nullable: false),
                    infoauto = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    fecha_fin_prestamo = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allianz_cartera_det", x => x.id);
                    table.ForeignKey(
                        name: "fk_allianz_cartera_det",
                        column: x => x.id_allianz_cartera_enc,
                        principalTable: "allianz_cartera_enc",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_allianz_cartera_det_id_allianz_cartera_enc",
                table: "allianz_cartera_det",
                column: "id_allianz_cartera_enc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "allianz_cartera_det");

            migrationBuilder.DropTable(
                name: "allianz_cartera_enc");
        }
    }
}
