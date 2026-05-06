using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalWorkshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTpExtendedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyProcesses_TechnologyProcesses_ParentVersionId",
                table: "TechnologyProcesses");

            migrationBuilder.DropIndex(
                name: "IX_TechnologyProcesses_ParentVersionId",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "InstructionText",
                table: "Transitions");

            migrationBuilder.DropColumn(
                name: "MediaUrls",
                table: "Transitions");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "ParentVersionId",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "VersionNumber",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "NormTime",
                table: "Operations");

            migrationBuilder.RenameColumn(
                name: "RequiredTools",
                table: "Transitions",
                newName: "ParamUnit");

            migrationBuilder.RenameColumn(
                name: "RequiredMaterials",
                table: "Transitions",
                newName: "MediaUrl");

            migrationBuilder.RenameColumn(
                name: "MinTime",
                table: "Transitions",
                newName: "MinParam");

            migrationBuilder.RenameColumn(
                name: "MaxTime",
                table: "Transitions",
                newName: "MaxParam");

            migrationBuilder.RenameColumn(
                name: "HashSum",
                table: "TechnologyProcesses",
                newName: "VersionHash");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TechnologyProcesses",
                newName: "VersionMinor");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "TechnologyProcesses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VersionMajor",
                table: "TechnologyProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NormTimeMinutes",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RequiredQualification",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnologyProcessId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_TechnologyProcesses_TechnologyProcessId",
                        column: x => x.TechnologyProcessId,
                        principalTable: "TechnologyProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BomItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitionId = table.Column<int>(type: "int", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BomItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BomItems_Transitions_TransitionId",
                        column: x => x.TransitionId,
                        principalTable: "Transitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToolRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitionId = table.Column<int>(type: "int", nullable: false),
                    ToolCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationIntervalDays = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToolRequirements_Transitions_TransitionId",
                        column: x => x.TransitionId,
                        principalTable: "Transitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TechnologyProcessId",
                table: "AuditLogs",
                column: "TechnologyProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_BomItems_TransitionId",
                table: "BomItems",
                column: "TransitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolRequirements_TransitionId",
                table: "ToolRequirements",
                column: "TransitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BomItems");

            migrationBuilder.DropTable(
                name: "ToolRequirements");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "VersionMajor",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "NormTimeMinutes",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "RequiredQualification",
                table: "Operations");

            migrationBuilder.RenameColumn(
                name: "ParamUnit",
                table: "Transitions",
                newName: "RequiredTools");

            migrationBuilder.RenameColumn(
                name: "MinParam",
                table: "Transitions",
                newName: "MinTime");

            migrationBuilder.RenameColumn(
                name: "MediaUrl",
                table: "Transitions",
                newName: "RequiredMaterials");

            migrationBuilder.RenameColumn(
                name: "MaxParam",
                table: "Transitions",
                newName: "MaxTime");

            migrationBuilder.RenameColumn(
                name: "VersionMinor",
                table: "TechnologyProcesses",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "VersionHash",
                table: "TechnologyProcesses",
                newName: "HashSum");

            migrationBuilder.AddColumn<string>(
                name: "InstructionText",
                table: "Transitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaUrls",
                table: "Transitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "TechnologyProcesses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentVersionId",
                table: "TechnologyProcesses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "TechnologyProcesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VersionNumber",
                table: "TechnologyProcesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "NormTime",
                table: "Operations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyProcesses_ParentVersionId",
                table: "TechnologyProcesses",
                column: "ParentVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyProcesses_TechnologyProcesses_ParentVersionId",
                table: "TechnologyProcesses",
                column: "ParentVersionId",
                principalTable: "TechnologyProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
