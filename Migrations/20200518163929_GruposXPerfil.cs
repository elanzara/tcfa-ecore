using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class GruposXPerfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "l_acc_grupos_x_perfil",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_origen = table.Column<int>(nullable: true),
                    id_acc_perfil = table.Column<int>(nullable: false),
                    id_acc_grupo = table.Column<int>(nullable: false),
                    creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
                    autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
                    accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_l_acc_grupos_x_perfil", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_acc_grupos_x_perfil",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_origen = table.Column<int>(nullable: true),
                    id_acc_perfil = table.Column<int>(nullable: false),
                    id_acc_grupo = table.Column<int>(nullable: false),
                    creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
                    autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
                    modifica = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_acc_grupos_x_perfil", x => x.id);
                    table.ForeignKey(
                        name: "fk_m_acc_grupos_x_perfil_grupo",
                        column: x => x.id_acc_grupo,
                        principalTable: "acc_grupos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_m_acc_grupos_x_perfil_perfil",
                        column: x => x.id_acc_perfil,
                        principalTable: "acc_perfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_m_acc_grupos_x_perfil_id_origen",
                        column: x => x.id_origen,
                        principalTable: "acc_grupos_x_perfil",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "n_acc_grupos_x_perfil",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_origen = table.Column<int>(nullable: true),
                    id_acc_perfil = table.Column<int>(nullable: false),
                    id_acc_grupo = table.Column<int>(nullable: false),
                    creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
                    autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
                    accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_n_acc_grupos_x_perfil", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_acc_grupos_x_perfil_id_acc_grupo",
                table: "m_acc_grupos_x_perfil",
                column: "id_acc_grupo");

            migrationBuilder.CreateIndex(
                name: "IX_m_acc_grupos_x_perfil_id_acc_perfil",
                table: "m_acc_grupos_x_perfil",
                column: "id_acc_perfil");

            migrationBuilder.CreateIndex(
                name: "IX_m_acc_grupos_x_perfil_id_origen",
                table: "m_acc_grupos_x_perfil",
                column: "id_origen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "l_acc_grupos_x_perfil");

            migrationBuilder.DropTable(
                name: "m_acc_grupos_x_perfil");

            migrationBuilder.DropTable(
                name: "n_acc_grupos_x_perfil");
        }
    }
}
