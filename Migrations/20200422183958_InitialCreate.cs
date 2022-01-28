using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "acc_acciones",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        codigo = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_acciones", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_grupos",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_grupos", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_modulos",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        id_acc_modulo = table.Column<int>(nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_modulos", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_menues_padre",
            //            column: x => x.id_acc_modulo,
            //            principalTable: "acc_modulos",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_perfiles",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_perfiles", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_programas",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        objeto = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
            //        parametros = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
            //        entidad = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_programas", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_tipos_eventos",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        tipo = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_tipos_eventos", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "l_acc_grupos",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_origen = table.Column<int>(nullable: true),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
            //        accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_l_acc_grupos", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "l_acc_modulos",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_origen = table.Column<int>(nullable: true),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        id_acc_modulo = table.Column<int>(nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
            //        accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_l_acc_modulos", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "l_acc_perfiles",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_origen = table.Column<int>(nullable: true),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
            //        accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_l_acc_perfiles", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "m_acc_grupos",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_origen = table.Column<int>(nullable: false),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
            //        modifica = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_m_acc_grupos", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "n_acc_grupos",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_origen = table.Column<int>(nullable: true),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
            //        accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_n_acc_grupos", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "n_acc_modulos",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_origen = table.Column<int>(nullable: true),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        id_acc_modulo = table.Column<int>(nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
            //        accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_n_acc_modulos", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "n_acc_perfiles",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_origen = table.Column<int>(nullable: true),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
            //        accionsql = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_n_acc_perfiles", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "m_acc_modulos",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_origen = table.Column<int>(nullable: true),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        id_acc_modulo = table.Column<int>(nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
            //        modifica = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_m_acc_modulos", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_id_acc_modulo",
            //            column: x => x.id_acc_modulo,
            //            principalTable: "acc_modulos",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "fk_m_acc_modulos",
            //            column: x => x.id_origen,
            //            principalTable: "acc_modulos",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_grupos_x_perfil",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_acc_perfil = table.Column<int>(nullable: false),
            //        id_acc_grupo = table.Column<int>(nullable: false),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_grupos_x_perfil", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_acc_grupos_x_perfil_modulos",
            //            column: x => x.id_acc_grupo,
            //            principalTable: "acc_grupos",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "fk_acc_grupos_x_perfil_perfiles",
            //            column: x => x.id_acc_perfil,
            //            principalTable: "acc_perfiles",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_usuarios",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_acc_perfil = table.Column<int>(nullable: false),
            //        security_identifier = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
            //        ad_cuenta = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
            //        apellido = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
            //        nombres = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
            //        max_cantidad_conexiones = table.Column<int>(nullable: false),
            //        bloqueado = table.Column<int>(nullable: false),
            //        sucursal = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
            //        ultima_conexion = table.Column<DateTime>(type: "datetime", nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_usuarios", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_acc_usuarios_perfiles",
            //            column: x => x.id_acc_perfil,
            //            principalTable: "acc_perfiles",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "m_acc_perfiles",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_origen = table.Column<int>(nullable: true),
            //        codigo = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
            //        descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true),
            //        modifica = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_m_acc_perfiles", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_m_acc_perfiles",
            //            column: x => x.id_origen,
            //            principalTable: "acc_perfiles",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_programas_acciones",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_acc_programa = table.Column<int>(nullable: false),
            //        id_acc_accion = table.Column<int>(nullable: false),
            //        origen = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_programas_acciones", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_acc_programas_acciones_acciones",
            //            column: x => x.id_acc_accion,
            //            principalTable: "acc_acciones",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "fk_acc_programas_acciones_programas",
            //            column: x => x.id_acc_programa,
            //            principalTable: "acc_programas",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_programas_x_modulos",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_acc_programa = table.Column<int>(nullable: false),
            //        id_acc_modulo = table.Column<int>(nullable: false),
            //        icono = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_programas_x_modulos", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_acc_modulos_programas_x_modulos",
            //            column: x => x.id_acc_modulo,
            //            principalTable: "acc_modulos",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "fk_acc_programas_x_modulos",
            //            column: x => x.id_acc_programa,
            //            principalTable: "acc_programas",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_eventos",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_acc_usuario = table.Column<int>(nullable: false),
            //        id_acc_tipo_evento = table.Column<int>(nullable: false),
            //        contexto = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        ocurrido_en = table.Column<DateTime>(type: "datetime", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_eventos", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_acc_eventos_tipos_eventos",
            //            column: x => x.id_acc_tipo_evento,
            //            principalTable: "acc_tipos_eventos",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "fk_acc_eventos_usuarios",
            //            column: x => x.id_acc_usuario,
            //            principalTable: "acc_usuarios",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_programas_favoritos_x_usuario",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_acc_programa = table.Column<int>(nullable: false),
            //        id_acc_usuario = table.Column<int>(nullable: false),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_programas_favoritos_x_usuario", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_acc_programas_favoritos_x_usuario_prog",
            //            column: x => x.id_acc_programa,
            //            principalTable: "acc_programas",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "fk_acc_programas_favoritos_x_usuario_usr",
            //            column: x => x.id_acc_usuario,
            //            principalTable: "acc_usuarios",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_programas_recientes_x_usuario",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_acc_programa = table.Column<int>(nullable: false),
            //        id_acc_usuario = table.Column<int>(nullable: false),
            //        fecha = table.Column<DateTime>(type: "datetime", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_programas_recientes_x_usuario", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_acc_programas_recientes_x_usuario_prog",
            //            column: x => x.id_acc_programa,
            //            principalTable: "acc_programas",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "fk_acc_programas_recientes_x_usuario_usr",
            //            column: x => x.id_acc_usuario,
            //            principalTable: "acc_usuarios",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_programas_x_usuario",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        tipo = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
            //        id_acc_programa = table.Column<int>(nullable: false),
            //        id_acc_usuario = table.Column<int>(nullable: false),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_programas_x_usuario", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_acc_programas_x_usuario_prog",
            //            column: x => x.id_acc_programa,
            //            principalTable: "acc_programas",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "fk_acc_programas_x_usuario_usr",
            //            column: x => x.id_acc_usuario,
            //            principalTable: "acc_usuarios",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_usuarios_sesiones",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_acc_usuario = table.Column<int>(nullable: false),
            //        token = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        finaliza_en = table.Column<DateTime>(type: "datetime", nullable: true),
            //        ultima_conexion = table.Column<DateTime>(type: "datetime", nullable: true),
            //        eventos = table.Column<string>(unicode: false, maxLength: 1, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_usuarios_sesiones", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_acc_usuarios_sesiones",
            //            column: x => x.id_acc_usuario,
            //            principalTable: "acc_usuarios",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "acc_programas_acciones_x_grupo",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        id_programa_accion = table.Column<int>(nullable: false),
            //        id_acc_grupo = table.Column<int>(nullable: false),
            //        icono = table.Column<string>(type: "text", nullable: true),
            //        creado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        creado_en = table.Column<DateTime>(type: "datetime", nullable: false),
            //        autorizado_por = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        autorizado_en = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_acc_programas_acciones_x_grupo", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_acc_programas_acciones_x_grupo_grupo",
            //            column: x => x.id_acc_grupo,
            //            principalTable: "acc_grupos",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "fk_acc_programas_acciones_x_grupo_accion",
            //            column: x => x.id_programa_accion,
            //            principalTable: "acc_programas_acciones",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "uk_acciones",
            //    table: "acc_acciones",
            //    column: "codigo",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_eventos_id_acc_tipo_evento",
            //    table: "acc_eventos",
            //    column: "id_acc_tipo_evento");

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_eventos_id_acc_usuario",
            //    table: "acc_eventos",
            //    column: "id_acc_usuario");

            //migrationBuilder.CreateIndex(
            //    name: "uk_grupos",
            //    table: "acc_grupos",
            //    column: "codigo",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_grupos_x_perfil_id_acc_grupo",
            //    table: "acc_grupos_x_perfil",
            //    column: "id_acc_grupo");

            //migrationBuilder.CreateIndex(
            //    name: "uk_grupos_x_perfil",
            //    table: "acc_grupos_x_perfil",
            //    columns: new[] { "id_acc_perfil", "id_acc_grupo" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "uk_modulos",
            //    table: "acc_modulos",
            //    column: "codigo",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_modulos_id_acc_modulo",
            //    table: "acc_modulos",
            //    column: "id_acc_modulo");

            //migrationBuilder.CreateIndex(
            //    name: "uk_perfiles",
            //    table: "acc_perfiles",
            //    column: "codigo",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "uk_programas",
            //    table: "acc_programas",
            //    column: "codigo",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_programas_acciones_id_acc_accion",
            //    table: "acc_programas_acciones",
            //    column: "id_acc_accion");

            //migrationBuilder.CreateIndex(
            //    name: "uk_programas_acciones",
            //    table: "acc_programas_acciones",
            //    columns: new[] { "id_acc_programa", "id_acc_accion", "origen" },
            //    unique: true,
            //    filter: "[origen] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_programas_acciones_x_grupo_id_programa_accion",
            //    table: "acc_programas_acciones_x_grupo",
            //    column: "id_programa_accion");

            //migrationBuilder.CreateIndex(
            //    name: "uk_programas_acciones_x_grupo",
            //    table: "acc_programas_acciones_x_grupo",
            //    columns: new[] { "id_acc_grupo", "id_programa_accion" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_programas_favoritos_x_usuario_id_acc_usuario",
            //    table: "acc_programas_favoritos_x_usuario",
            //    column: "id_acc_usuario");

            //migrationBuilder.CreateIndex(
            //    name: "uk_acc_programas_favoritos_x_usuario",
            //    table: "acc_programas_favoritos_x_usuario",
            //    columns: new[] { "id_acc_programa", "id_acc_usuario" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_programas_recientes_x_usuario_id_acc_programa",
            //    table: "acc_programas_recientes_x_usuario",
            //    column: "id_acc_programa");

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_programas_recientes_x_usuario_id_acc_usuario",
            //    table: "acc_programas_recientes_x_usuario",
            //    column: "id_acc_usuario");

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_programas_x_modulos_id_acc_modulo",
            //    table: "acc_programas_x_modulos",
            //    column: "id_acc_modulo");

            //migrationBuilder.CreateIndex(
            //    name: "uk_programas_x_modulos",
            //    table: "acc_programas_x_modulos",
            //    columns: new[] { "id_acc_programa", "id_acc_modulo" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_programas_x_usuario_id_acc_usuario",
            //    table: "acc_programas_x_usuario",
            //    column: "id_acc_usuario");

            //migrationBuilder.CreateIndex(
            //    name: "uk_programas_x_usuario",
            //    table: "acc_programas_x_usuario",
            //    columns: new[] { "id_acc_programa", "id_acc_usuario" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "uk_eventos",
            //    table: "acc_tipos_eventos",
            //    column: "codigo",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "uk_ad_cuenta",
            //    table: "acc_usuarios",
            //    column: "ad_cuenta",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_usuarios_id_acc_perfil",
            //    table: "acc_usuarios",
            //    column: "id_acc_perfil");

            //migrationBuilder.CreateIndex(
            //    name: "uk_security_identifier",
            //    table: "acc_usuarios",
            //    column: "security_identifier",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_acc_usuarios_sesiones_id_acc_usuario",
            //    table: "acc_usuarios_sesiones",
            //    column: "id_acc_usuario");

            //migrationBuilder.CreateIndex(
            //    name: "IX_m_acc_modulos_id_acc_modulo",
            //    table: "m_acc_modulos",
            //    column: "id_acc_modulo");

            //migrationBuilder.CreateIndex(
            //    name: "IX_m_acc_modulos_id_origen",
            //    table: "m_acc_modulos",
            //    column: "id_origen");

            //migrationBuilder.CreateIndex(
            //    name: "IX_m_acc_perfiles_id_origen",
            //    table: "m_acc_perfiles",
            //    column: "id_origen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "acc_eventos");

            //migrationBuilder.DropTable(
            //    name: "acc_grupos_x_perfil");

            //migrationBuilder.DropTable(
            //    name: "acc_programas_acciones_x_grupo");

            //migrationBuilder.DropTable(
            //    name: "acc_programas_favoritos_x_usuario");

            //migrationBuilder.DropTable(
            //    name: "acc_programas_recientes_x_usuario");

            //migrationBuilder.DropTable(
            //    name: "acc_programas_x_modulos");

            //migrationBuilder.DropTable(
            //    name: "acc_programas_x_usuario");

            //migrationBuilder.DropTable(
            //    name: "acc_usuarios_sesiones");

            //migrationBuilder.DropTable(
            //    name: "l_acc_grupos");

            //migrationBuilder.DropTable(
            //    name: "l_acc_modulos");

            //migrationBuilder.DropTable(
            //    name: "l_acc_perfiles");

            //migrationBuilder.DropTable(
            //    name: "m_acc_grupos");

            //migrationBuilder.DropTable(
            //    name: "m_acc_modulos");

            //migrationBuilder.DropTable(
            //    name: "m_acc_perfiles");

            //migrationBuilder.DropTable(
            //    name: "n_acc_grupos");

            //migrationBuilder.DropTable(
            //    name: "n_acc_modulos");

            //migrationBuilder.DropTable(
            //    name: "n_acc_perfiles");

            //migrationBuilder.DropTable(
            //    name: "acc_tipos_eventos");

            //migrationBuilder.DropTable(
            //    name: "acc_grupos");

            //migrationBuilder.DropTable(
            //    name: "acc_programas_acciones");

            //migrationBuilder.DropTable(
            //    name: "acc_usuarios");

            //migrationBuilder.DropTable(
            //    name: "acc_modulos");

            //migrationBuilder.DropTable(
            //    name: "acc_acciones");

            //migrationBuilder.DropTable(
            //    name: "acc_programas");

            //migrationBuilder.DropTable(
            //    name: "acc_perfiles");
        }
    }
}
