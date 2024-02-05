using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updatedinstallmenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentPlanId",
                table: "PaymentPlanInstallments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlanInstallments_PaymentPlanId",
                table: "PaymentPlanInstallments",
                column: "PaymentPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlanInstallments_PaymentPlans_PaymentPlanId",
                table: "PaymentPlanInstallments",
                column: "PaymentPlanId",
                principalTable: "PaymentPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlanInstallments_PaymentPlans_PaymentPlanId",
                table: "PaymentPlanInstallments");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPlanInstallments_PaymentPlanId",
                table: "PaymentPlanInstallments");

            migrationBuilder.DropColumn(
                name: "PaymentPlanId",
                table: "PaymentPlanInstallments");
        }
    }
}
