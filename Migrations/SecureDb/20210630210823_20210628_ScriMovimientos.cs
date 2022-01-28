using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCore.Migrations.SecureDb
{
    public partial class _20210628_ScriMovimientos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "scri_movimientos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HasError = table.Column<bool>(nullable: false),
                    HasWarning = table.Column<bool>(nullable: false),
                    HasInformation = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scri_movimientos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scri_list_job_summary",
                columns: table => new
                {
                    IdScriMovimientos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScriListJobSummary_IdScriMovimientos = table.Column<int>(nullable: false),
                    OfferingPlan = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    PolicyPeriodID = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    ScopeCoverage = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    TransactionJob = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Subtype = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime", nullable: false),
                    PolicyStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PolicyNumber = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scri_list_job_summary", x => x.IdScriMovimientos);
                    table.ForeignKey(
                        name: "fk_listjobsummary_movimientos",
                        column: x => x.ScriListJobSummary_IdScriMovimientos,
                        principalTable: "scri_movimientos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "scri_messages",
                columns: table => new
                {
                    ErrorLevel = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdScriMovimientos = table.Column<int>(nullable: false),
                    NombreServicio = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    VersionServicio = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    MessageBeautiful = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    StackTrace = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    ScriMessages_ErrorLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scri_messages", x => x.ErrorLevel);
                    table.ForeignKey(
                        name: "fk_mensajes_movimientos",
                        column: x => x.IdScriMovimientos,
                        principalTable: "scri_movimientos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "scri_offering",
                columns: table => new
                {
                    IdScriListJobSummary = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScriOffering_IdScriListJobSummary = table.Column<int>(nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scri_offering", x => x.IdScriListJobSummary);
                    table.ForeignKey(
                        name: "fk_offering_listjobsummary",
                        column: x => x.ScriOffering_IdScriListJobSummary,
                        principalTable: "scri_list_job_summary",
                        principalColumn: "IdScriMovimientos",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "scri_policy_type",
                columns: table => new
                {
                    IdScriListJobSummary = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScriPolicyType_IdScriListJobSummary = table.Column<int>(nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scri_policy_type", x => x.IdScriListJobSummary);
                    table.ForeignKey(
                        name: "fk_policytype_listjobsummary",
                        column: x => x.ScriPolicyType_IdScriListJobSummary,
                        principalTable: "scri_list_job_summary",
                        principalColumn: "IdScriMovimientos",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "scri_product",
                columns: table => new
                {
                    IdScriListJobSummary = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScriProduct_IdScriListJobSummary = table.Column<int>(nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scri_product", x => x.IdScriListJobSummary);
                    table.ForeignKey(
                        name: "fk_product_listjobsummary",
                        column: x => x.ScriProduct_IdScriListJobSummary,
                        principalTable: "scri_list_job_summary",
                        principalColumn: "IdScriMovimientos",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_scri_list_job_summary_ScriListJobSummary_IdScriMovimientos",
                table: "scri_list_job_summary",
                column: "ScriListJobSummary_IdScriMovimientos");

            migrationBuilder.CreateIndex(
                name: "IX_scri_messages_IdScriMovimientos",
                table: "scri_messages",
                column: "IdScriMovimientos");

            migrationBuilder.CreateIndex(
                name: "IX_scri_offering_ScriOffering_IdScriListJobSummary",
                table: "scri_offering",
                column: "ScriOffering_IdScriListJobSummary");

            migrationBuilder.CreateIndex(
                name: "IX_scri_policy_type_ScriPolicyType_IdScriListJobSummary",
                table: "scri_policy_type",
                column: "ScriPolicyType_IdScriListJobSummary");

            migrationBuilder.CreateIndex(
                name: "IX_scri_product_ScriProduct_IdScriListJobSummary",
                table: "scri_product",
                column: "ScriProduct_IdScriListJobSummary");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scri_messages");

            migrationBuilder.DropTable(
                name: "scri_offering");

            migrationBuilder.DropTable(
                name: "scri_policy_type");

            migrationBuilder.DropTable(
                name: "scri_product");

            migrationBuilder.DropTable(
                name: "scri_list_job_summary");

            migrationBuilder.DropTable(
                name: "scri_movimientos");
        }
    }
}
