using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedInstallmenttypeol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentType",
                table: "PaymentPlans");

            migrationBuilder.AddColumn<Guid>(
                name: "InstallmentTypeId",
                table: "PaymentPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlans_InstallmentTypeId",
                table: "PaymentPlans",
                column: "InstallmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlans_InstallmentTypes_InstallmentTypeId",
                table: "PaymentPlans",
                column: "InstallmentTypeId",
                principalTable: "InstallmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlans_InstallmentTypes_InstallmentTypeId",
                table: "PaymentPlans");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPlans_InstallmentTypeId",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "InstallmentTypeId",
                table: "PaymentPlans");

            migrationBuilder.AddColumn<Guid>(
                name: "InstallmentType",
                table: "PaymentPlans",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
