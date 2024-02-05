using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedpaymentplanandtransactionaccordingly2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentPlanTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaidBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidByTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionModeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetAmount = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPlanTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPlanTransactions_PaymentPlans_PaymentPlanId",
                        column: x => x.PaymentPlanId,
                        principalTable: "PaymentPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentPlanTransactions_Tenants_PaidByTenantId",
                        column: x => x.PaidByTenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentPlanTransactions_TransactionModes_TransactionModeId",
                        column: x => x.TransactionModeId,
                        principalTable: "TransactionModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlanTransactions_PaidByTenantId",
                table: "PaymentPlanTransactions",
                column: "PaidByTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlanTransactions_PaymentPlanId",
                table: "PaymentPlanTransactions",
                column: "PaymentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlanTransactions_TransactionModeId",
                table: "PaymentPlanTransactions",
                column: "TransactionModeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentPlanTransactions");
        }
    }
}
