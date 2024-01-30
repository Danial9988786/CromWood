using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedchangelogtableupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ChangeLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ChangeLogs");
        }
    }
}
