using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedreporttables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Favourite = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortByAsc = table.Column<bool>(type: "bit", nullable: false),
                    SortBy2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortBy2Asc = table.Column<bool>(type: "bit", nullable: false),
                    DateFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZeroCurrencyBlank = table.Column<bool>(type: "bit", nullable: false),
                    HideCurrencySymbol = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomReportAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    HeaderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alignment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Append = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prepend = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomReportAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomReportAttributes_CustomReports_CustomReportId",
                        column: x => x.CustomReportId,
                        principalTable: "CustomReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomReportConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomReportConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomReportConditions_CustomReports_CustomReportId",
                        column: x => x.CustomReportId,
                        principalTable: "CustomReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomReportAttributes_CustomReportId",
                table: "CustomReportAttributes",
                column: "CustomReportId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomReportConditions_CustomReportId",
                table: "CustomReportConditions",
                column: "CustomReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomReportAttributes");

            migrationBuilder.DropTable(
                name: "CustomReportConditions");

            migrationBuilder.DropTable(
                name: "CustomReports");
        }
    }
}
