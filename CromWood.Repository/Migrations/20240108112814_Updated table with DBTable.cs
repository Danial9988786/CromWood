using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedtablewithDBTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "TenancyTenants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TenancyTenants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "TenancyTenants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "TenancyTenants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "TenancyNotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TenancyNotes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "TenancyNotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "TenancyNotes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "TenancyDocuments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TenancyDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "TenancyDocuments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "TenancyDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "PropertyInsurances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PropertyInsurances",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "PropertyInsurances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "PropertyInsurances",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "PropertyAmenities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PropertyAmenities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "PropertyAmenities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "PropertyAmenities",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TenancyTenants");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TenancyTenants");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TenancyTenants");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TenancyTenants");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TenancyNotes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TenancyNotes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TenancyNotes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TenancyNotes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TenancyDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TenancyDocuments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TenancyDocuments");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TenancyDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PropertyInsurances");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PropertyInsurances");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PropertyInsurances");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "PropertyInsurances");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "PropertyAmenities");
        }
    }
}
