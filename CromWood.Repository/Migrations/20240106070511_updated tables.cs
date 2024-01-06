using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_ClaimTypes_ClaimTypeId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Courts_CourtId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Notice_Concerns_ConcernId",
                table: "Notice");

            migrationBuilder.DropForeignKey(
                name: "FK_Notice_Sections_SectionId",
                table: "Notice");

            migrationBuilder.DropForeignKey(
                name: "FK_Notice_Tenants_TenantId",
                table: "Notice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notice",
                table: "Notice");

            migrationBuilder.RenameTable(
                name: "Notice",
                newName: "Notices");

            migrationBuilder.RenameIndex(
                name: "IX_Notice_TenantId",
                table: "Notices",
                newName: "IX_Notices_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Notice_SectionId",
                table: "Notices",
                newName: "IX_Notices_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Notice_ConcernId",
                table: "Notices",
                newName: "IX_Notices_ConcernId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HearingDate",
                table: "Claims",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtId",
                table: "Claims",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClaimTypeId",
                table: "Claims",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServedAndPictured",
                table: "Notices",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "Notices",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmailedOn",
                table: "Notices",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Notices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notices",
                table: "Notices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_ClaimTypes_ClaimTypeId",
                table: "Claims",
                column: "ClaimTypeId",
                principalTable: "ClaimTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Courts_CourtId",
                table: "Claims",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_Concerns_ConcernId",
                table: "Notices",
                column: "ConcernId",
                principalTable: "Concerns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_Sections_SectionId",
                table: "Notices",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_Tenants_TenantId",
                table: "Notices",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_ClaimTypes_ClaimTypeId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Courts_CourtId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Notices_Concerns_ConcernId",
                table: "Notices");

            migrationBuilder.DropForeignKey(
                name: "FK_Notices_Sections_SectionId",
                table: "Notices");

            migrationBuilder.DropForeignKey(
                name: "FK_Notices_Tenants_TenantId",
                table: "Notices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notices",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Notices");

            migrationBuilder.RenameTable(
                name: "Notices",
                newName: "Notice");

            migrationBuilder.RenameIndex(
                name: "IX_Notices_TenantId",
                table: "Notice",
                newName: "IX_Notice_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Notices_SectionId",
                table: "Notice",
                newName: "IX_Notice_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Notices_ConcernId",
                table: "Notice",
                newName: "IX_Notice_ConcernId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HearingDate",
                table: "Claims",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtId",
                table: "Claims",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClaimTypeId",
                table: "Claims",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServedAndPictured",
                table: "Notice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "Notice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmailedOn",
                table: "Notice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notice",
                table: "Notice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_ClaimTypes_ClaimTypeId",
                table: "Claims",
                column: "ClaimTypeId",
                principalTable: "ClaimTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Courts_CourtId",
                table: "Claims",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notice_Concerns_ConcernId",
                table: "Notice",
                column: "ConcernId",
                principalTable: "Concerns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notice_Sections_SectionId",
                table: "Notice",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notice_Tenants_TenantId",
                table: "Notice",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
