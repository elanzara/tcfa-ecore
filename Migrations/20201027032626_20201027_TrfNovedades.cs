using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class _20201027_TrfNovedades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrfNovedades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Articulo = table.Column<int>(nullable: false),
                    ArticuloAnt = table.Column<int>(nullable: false),
                    CUIT = table.Column<string>(nullable: true),
                    Certificado = table.Column<string>(nullable: true),
                    CertificadoAnt = table.Column<string>(nullable: true),
                    CodigoPostal = table.Column<int>(nullable: false),
                    CondicionIVA = table.Column<string>(nullable: true),
                    CondicionIVAN = table.Column<int>(nullable: false),
                    DocNumero = table.Column<string>(nullable: true),
                    DocTipo = table.Column<int>(nullable: false),
                    Domicilio = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Empresa = table.Column<string>(nullable: true),
                    EmpresaAnt = table.Column<string>(nullable: true),
                    EstadoPoliza = table.Column<string>(nullable: true),
                    EstadoPolizaN = table.Column<string>(nullable: true),
                    Moneda = table.Column<string>(nullable: true),
                    RazonSocial = table.Column<string>(nullable: true),
                    SubCodigoPostal = table.Column<int>(nullable: false),
                    Sucursal = table.Column<string>(nullable: true),
                    SucursalAnt = table.Column<string>(nullable: true),
                    Suplemento = table.Column<int>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    TelefonoParticular = table.Column<string>(nullable: true),
                    VigenciaDesde = table.Column<string>(nullable: true),
                    VigenciaHasta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrfNovedades", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrfNovedades");
        }
    }
}
