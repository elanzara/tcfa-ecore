using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class TrfNovedades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrfNovedades",
                table: "TrfNovedades");

            migrationBuilder.RenameTable(
                name: "TrfNovedades",
                newName: "trf_novedades");

            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "trf_novedades",
                newName: "telefono");

            migrationBuilder.RenameColumn(
                name: "Suplemento",
                table: "trf_novedades",
                newName: "suplemento");

            migrationBuilder.RenameColumn(
                name: "Sucursal",
                table: "trf_novedades",
                newName: "sucursal");

            migrationBuilder.RenameColumn(
                name: "Moneda",
                table: "trf_novedades",
                newName: "moneda");

            migrationBuilder.RenameColumn(
                name: "Empresa",
                table: "trf_novedades",
                newName: "empresa");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "trf_novedades",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Domicilio",
                table: "trf_novedades",
                newName: "domicilio");

            migrationBuilder.RenameColumn(
                name: "Certificado",
                table: "trf_novedades",
                newName: "certificado");

            migrationBuilder.RenameColumn(
                name: "CUIT",
                table: "trf_novedades",
                newName: "cuit");

            migrationBuilder.RenameColumn(
                name: "Articulo",
                table: "trf_novedades",
                newName: "articulo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "trf_novedades",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "VigenciaHasta",
                table: "trf_novedades",
                newName: "vigencia_hasta");

            migrationBuilder.RenameColumn(
                name: "VigenciaDesde",
                table: "trf_novedades",
                newName: "vigencia_desde");

            migrationBuilder.RenameColumn(
                name: "TelefonoParticular",
                table: "trf_novedades",
                newName: "telefono_particular");

            migrationBuilder.RenameColumn(
                name: "SucursalAnt",
                table: "trf_novedades",
                newName: "Sucursal_ant");

            migrationBuilder.RenameColumn(
                name: "SubCodigoPostal",
                table: "trf_novedades",
                newName: "sub_codigo_postal");

            migrationBuilder.RenameColumn(
                name: "RazonSocial",
                table: "trf_novedades",
                newName: "razon_social");

            migrationBuilder.RenameColumn(
                name: "EstadoPolizaN",
                table: "trf_novedades",
                newName: "estado_poliza_n");

            migrationBuilder.RenameColumn(
                name: "EstadoPoliza",
                table: "trf_novedades",
                newName: "estado_poliza");

            migrationBuilder.RenameColumn(
                name: "EmpresaAnt",
                table: "trf_novedades",
                newName: "empresa_ant");

            migrationBuilder.RenameColumn(
                name: "DocTipo",
                table: "trf_novedades",
                newName: "doc_tipo");

            migrationBuilder.RenameColumn(
                name: "DocNumero",
                table: "trf_novedades",
                newName: "doc_numero");

            migrationBuilder.RenameColumn(
                name: "CondicionIVAN",
                table: "trf_novedades",
                newName: "condicion_ivan");

            migrationBuilder.RenameColumn(
                name: "CondicionIVA",
                table: "trf_novedades",
                newName: "condicion_iva");

            migrationBuilder.RenameColumn(
                name: "CodigoPostal",
                table: "trf_novedades",
                newName: "codigo_postal");

            migrationBuilder.RenameColumn(
                name: "CertificadoAnt",
                table: "trf_novedades",
                newName: "certificado_ant");

            migrationBuilder.RenameColumn(
                name: "ArticuloAnt",
                table: "trf_novedades",
                newName: "articulo_ant");

            migrationBuilder.AlterColumn<string>(
                name: "telefono",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sucursal",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "moneda",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "empresa",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "domicilio",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "certificado",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cuit",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "vigencia_hasta",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "vigencia_desde",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "telefono_particular",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sucursal_ant",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "razon_social",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "estado_poliza_n",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "estado_poliza",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "empresa_ant",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "doc_numero",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "condicion_iva",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "certificado_ant",
                table: "trf_novedades",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_trf_novedades",
                table: "trf_novedades",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_trf_novedades",
                table: "trf_novedades");

            migrationBuilder.RenameTable(
                name: "trf_novedades",
                newName: "TrfNovedades");

            migrationBuilder.RenameColumn(
                name: "telefono",
                table: "TrfNovedades",
                newName: "Telefono");

            migrationBuilder.RenameColumn(
                name: "suplemento",
                table: "TrfNovedades",
                newName: "Suplemento");

            migrationBuilder.RenameColumn(
                name: "sucursal",
                table: "TrfNovedades",
                newName: "Sucursal");

            migrationBuilder.RenameColumn(
                name: "moneda",
                table: "TrfNovedades",
                newName: "Moneda");

            migrationBuilder.RenameColumn(
                name: "empresa",
                table: "TrfNovedades",
                newName: "Empresa");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "TrfNovedades",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "domicilio",
                table: "TrfNovedades",
                newName: "Domicilio");

            migrationBuilder.RenameColumn(
                name: "certificado",
                table: "TrfNovedades",
                newName: "Certificado");

            migrationBuilder.RenameColumn(
                name: "cuit",
                table: "TrfNovedades",
                newName: "CUIT");

            migrationBuilder.RenameColumn(
                name: "articulo",
                table: "TrfNovedades",
                newName: "Articulo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TrfNovedades",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "vigencia_hasta",
                table: "TrfNovedades",
                newName: "VigenciaHasta");

            migrationBuilder.RenameColumn(
                name: "vigencia_desde",
                table: "TrfNovedades",
                newName: "VigenciaDesde");

            migrationBuilder.RenameColumn(
                name: "telefono_particular",
                table: "TrfNovedades",
                newName: "TelefonoParticular");

            migrationBuilder.RenameColumn(
                name: "Sucursal_ant",
                table: "TrfNovedades",
                newName: "SucursalAnt");

            migrationBuilder.RenameColumn(
                name: "sub_codigo_postal",
                table: "TrfNovedades",
                newName: "SubCodigoPostal");

            migrationBuilder.RenameColumn(
                name: "razon_social",
                table: "TrfNovedades",
                newName: "RazonSocial");

            migrationBuilder.RenameColumn(
                name: "estado_poliza_n",
                table: "TrfNovedades",
                newName: "EstadoPolizaN");

            migrationBuilder.RenameColumn(
                name: "estado_poliza",
                table: "TrfNovedades",
                newName: "EstadoPoliza");

            migrationBuilder.RenameColumn(
                name: "empresa_ant",
                table: "TrfNovedades",
                newName: "EmpresaAnt");

            migrationBuilder.RenameColumn(
                name: "doc_tipo",
                table: "TrfNovedades",
                newName: "DocTipo");

            migrationBuilder.RenameColumn(
                name: "doc_numero",
                table: "TrfNovedades",
                newName: "DocNumero");

            migrationBuilder.RenameColumn(
                name: "condicion_ivan",
                table: "TrfNovedades",
                newName: "CondicionIVAN");

            migrationBuilder.RenameColumn(
                name: "condicion_iva",
                table: "TrfNovedades",
                newName: "CondicionIVA");

            migrationBuilder.RenameColumn(
                name: "codigo_postal",
                table: "TrfNovedades",
                newName: "CodigoPostal");

            migrationBuilder.RenameColumn(
                name: "certificado_ant",
                table: "TrfNovedades",
                newName: "CertificadoAnt");

            migrationBuilder.RenameColumn(
                name: "articulo_ant",
                table: "TrfNovedades",
                newName: "ArticuloAnt");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sucursal",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Moneda",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Empresa",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Domicilio",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Certificado",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CUIT",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VigenciaHasta",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VigenciaDesde",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TelefonoParticular",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SucursalAnt",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RazonSocial",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EstadoPolizaN",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EstadoPoliza",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmpresaAnt",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocNumero",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CondicionIVA",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CertificadoAnt",
                table: "TrfNovedades",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrfNovedades",
                table: "TrfNovedades",
                column: "Id");
        }
    }
}
