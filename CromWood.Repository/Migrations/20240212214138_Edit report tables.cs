using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class Editreporttables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "CustomReports",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "CustomReports",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CustomReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "CustomReportConditions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomReportConditions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "CustomReportConditions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CustomReportConditions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "CustomReportAttributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CustomReportAttributes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "CustomReportAttributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CustomReportAttributes",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CustomReports");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CustomReports");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CustomReports");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CustomReports");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CustomReportConditions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CustomReportConditions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CustomReportConditions");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CustomReportConditions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CustomReportAttributes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CustomReportAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CustomReportAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CustomReportAttributes");
        }
    }
}
