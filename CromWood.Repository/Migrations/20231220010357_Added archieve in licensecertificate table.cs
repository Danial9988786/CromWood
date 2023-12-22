using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedarchieveinlicensecertificatetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archieved",
                table: "LicenseCertificates",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archieved",
                table: "LicenseCertificates");
        }
    }
}
