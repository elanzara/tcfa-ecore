using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class Usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "l_acc_usuarios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_origen = table.Column<int>(nullable: true),
                    id_acc_perfil = table.Column<int>(nullable: false),
                    security_identifier = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    ad_cuenta = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    apellido = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    nombres = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    max_cantidad_conexiones = table.Column<int>(nullable: false),
                    bloqueado = table.Column<int>(nullable: false),
                    sucursal = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    ultima_conexion = table.Column<DateTime>(type: "datetime", nullable: true),
                    creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
                    autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
                    accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_l_acc_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_acc_usuarios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_origen = table.Column<int>(nullable: true),
                    id_acc_perfil = table.Column<int>(nullable: false),
                    security_identifier = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    ad_cuenta = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    apellido = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    nombres = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    max_cantidad_conexiones = table.Column<int>(nullable: false),
                    bloqueado = table.Column<int>(nullable: false),
                    sucursal = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    ultima_conexion = table.Column<DateTime>(type: "datetime", nullable: true),
                    creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
                    autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
                    modifica = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_acc_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_id_acc_perfil_usuario",
                        column: x => x.id_acc_perfil,
                        principalTable: "acc_perfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_m_acc_perfiles_usuario",
                        column: x => x.id_origen,
                        principalTable: "acc_usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "n_acc_usuarios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_origen = table.Column<int>(nullable: true),
                    id_acc_perfil = table.Column<int>(nullable: false),
                    security_identifier = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    ad_cuenta = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    apellido = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    nombres = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    max_cantidad_conexiones = table.Column<int>(nullable: false),
                    bloqueado = table.Column<int>(nullable: false),
                    sucursal = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    ultima_conexion = table.Column<DateTime>(type: "datetime", nullable: true),
                    creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
                    autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
                    accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_n_acc_usuarios", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_acc_usuarios_id_acc_perfil",
                table: "m_acc_usuarios",
                column: "id_acc_perfil");

            migrationBuilder.CreateIndex(
                name: "IX_m_acc_usuarios_id_origen",
                table: "m_acc_usuarios",
                column: "id_origen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "l_acc_usuarios");

            migrationBuilder.DropTable(
                name: "m_acc_usuarios");

            migrationBuilder.DropTable(
                name: "n_acc_usuarios");
        }
    }
}
