using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class Editedcolumnsintransactionofpaymentmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlanTransactions_PaymentPlans_PaymentPlanId",
                table: "PaymentPlanTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlanTransactions_Tenants_PaidByTenantId",
                table: "PaymentPlanTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlanTransactions_TransactionModes_TransactionModeId",
                table: "PaymentPlanTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPlanTransactions_PaidByTenantId",
                table: "PaymentPlanTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPlanTransactions_TransactionModeId",
                table: "PaymentPlanTransactions");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "PaymentPlanTransactions");

            migrationBuilder.DropColumn(
                name: "PaidByTenantId",
                table: "PaymentPlanTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionModeId",
                table: "PaymentPlanTransactions");

            migrationBuilder.RenameColumn(
                name: "TransactionDescription",
                table: "PaymentPlanTransactions",
                newName: "ReferenceID");

            migrationBuilder.RenameColumn(
                name: "PaidBy",
                table: "PaymentPlanTransactions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "NetAmount",
                table: "PaymentPlanTransactions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "PaymentPlanTransactions",
                newName: "PaymentDate");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentPlanId",
                table: "PaymentPlanTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlanTransactions_PaymentPlans_PaymentPlanId",
                table: "PaymentPlanTransactions",
                column: "PaymentPlanId",
                principalTable: "PaymentPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlanTransactions_PaymentPlans_PaymentPlanId",
                table: "PaymentPlanTransactions");

            migrationBuilder.RenameColumn(
                name: "ReferenceID",
                table: "PaymentPlanTransactions",
                newName: "TransactionDescription");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "PaymentPlanTransactions",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "PaymentPlanTransactions",
                newName: "PaidBy");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "PaymentPlanTransactions",
                newName: "NetAmount");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentPlanId",
                table: "PaymentPlanTransactions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "PaymentPlanTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaidByTenantId",
                table: "PaymentPlanTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionModeId",
                table: "PaymentPlanTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlanTransactions_PaidByTenantId",
                table: "PaymentPlanTransactions",
                column: "PaidByTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlanTransactions_TransactionModeId",
                table: "PaymentPlanTransactions",
                column: "TransactionModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlanTransactions_PaymentPlans_PaymentPlanId",
                table: "PaymentPlanTransactions",
                column: "PaymentPlanId",
                principalTable: "PaymentPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlanTransactions_Tenants_PaidByTenantId",
                table: "PaymentPlanTransactions",
                column: "PaidByTenantId",
                principalTable: "Tenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlanTransactions_TransactionModes_TransactionModeId",
                table: "PaymentPlanTransactions",
                column: "TransactionModeId",
                principalTable: "TransactionModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
