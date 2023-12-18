using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedfinancialprogramasforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "FinancialPrgoramId",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_FinancialPrgoramId",
                table: "Assets",
                column: "FinancialPrgoramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_FinancialPrgorams_FinancialPrgoramId",
                table: "Assets",
                column: "FinancialPrgoramId",
                principalTable: "FinancialPrgorams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_FinancialPrgorams_FinancialPrgoramId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_FinancialPrgoramId",
                table: "Assets");

            migrationBuilder.AlterColumn<Guid>(
                name: "FinancialPrgoramId",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
