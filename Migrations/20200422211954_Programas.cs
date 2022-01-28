using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class Programas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "l_acc_programas",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_origen = table.Column<int>(nullable: true),
                    codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    objeto = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    parametros = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    entidad = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
                    autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
                    accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_l_acc_programas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_acc_programas",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_origen = table.Column<int>(nullable: true),
                    codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    objeto = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    parametros = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    entidad = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
                    autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
                    modifica = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_m_acc_programas", x => x.id);
                    table.ForeignKey(
                        name: "fk_m_acc_programas",
                        column: x => x.id_origen,
                        principalTable: "acc_programas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "n_acc_programas",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_origen = table.Column<int>(nullable: true),
                    codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    objeto = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    parametros = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    entidad = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
                    autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
                    accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_n_acc_programas", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_acc_programas_id_origen",
                table: "m_acc_programas",
                column: "id_origen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "l_acc_programas");

            migrationBuilder.DropTable(
                name: "m_acc_programas");

            migrationBuilder.DropTable(
                name: "n_acc_programas");
        }
    }
}
