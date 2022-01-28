using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class TrfVehiculoDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trf_vehiculo_datos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_trf_novedades = table.Column<int>(nullable: false),
                    anio = table.Column<int>(nullable: false),
                    ceroKm = table.Column<int>(nullable: false),
                    chasis = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    cobertura = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    dominio = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    marca = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    marcaia = table.Column<int>(nullable: false),
                    modelo = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    modeloia = table.Column<int>(nullable: false),
                    motor = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    origen = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    sub_modelo = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    suma_asegurada = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    tipo = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    tipon = table.Column<int>(nullable: false),
                    uso = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    uson = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trf_vehiculo_datos", x => x.id);
                    table.ForeignKey(
                        name: "fk_trf_vehiculo_datos",
                        column: x => x.id_trf_novedades,
                        principalTable: "trf_novedades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trf_vehiculo_datos_id_trf_novedades",
                table: "trf_vehiculo_datos",
                column: "id_trf_novedades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trf_vehiculo_datos");
        }
    }
}
