using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updatekeytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PropertyKeyType",
                table: "PropertyKeys",
                newName: "PropertyKeyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyKeys_PropertyKeyTypeId",
                table: "PropertyKeys",
                column: "PropertyKeyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyKeys_PropertyKeyTypes_PropertyKeyTypeId",
                table: "PropertyKeys",
                column: "PropertyKeyTypeId",
                principalTable: "PropertyKeyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyKeys_PropertyKeyTypes_PropertyKeyTypeId",
                table: "PropertyKeys");

            migrationBuilder.DropIndex(
                name: "IX_PropertyKeys_PropertyKeyTypeId",
                table: "PropertyKeys");

            migrationBuilder.RenameColumn(
                name: "PropertyKeyTypeId",
                table: "PropertyKeys",
                newName: "PropertyKeyType");
        }
    }
}
