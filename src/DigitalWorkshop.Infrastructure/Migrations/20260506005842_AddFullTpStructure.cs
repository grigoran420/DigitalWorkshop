using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalWorkshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFullTpStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Version",
                table: "TechnologyProcesses",
                newName: "VersionNumber");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "TechnologyProcesses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "TechnologyProcesses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "TechnologyProcesses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "TechnologyProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HashSum",
                table: "TechnologyProcesses",
                type: "nvarchar(max)",
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

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnologyProcessId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_TechnologyProcesses_TechnologyProcessId",
                        column: x => x.TechnologyProcessId,
                        principalTable: "TechnologyProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaUrls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredMaterials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredTools = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinTime = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxTime = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transitions_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyProcesses_ParentVersionId",
                table: "TechnologyProcesses",
                column: "ParentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_TechnologyProcessId",
                table: "Operations",
                column: "TechnologyProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Transitions_OperationId",
                table: "Transitions",
                column: "OperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyProcesses_TechnologyProcesses_ParentVersionId",
                table: "TechnologyProcesses",
                column: "ParentVersionId",
                principalTable: "TechnologyProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyProcesses_TechnologyProcesses_ParentVersionId",
                table: "TechnologyProcesses");

            migrationBuilder.DropTable(
                name: "Transitions");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_TechnologyProcesses_ParentVersionId",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "HashSum",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "ParentVersionId",
                table: "TechnologyProcesses");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "TechnologyProcesses");

            migrationBuilder.RenameColumn(
                name: "VersionNumber",
                table: "TechnologyProcesses",
                newName: "Version");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TechnologyProcesses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
