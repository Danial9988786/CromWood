using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CromWood.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedtableforPropertyAssesment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetailItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyAssesments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InspectionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InspectorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InspectionNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAssesments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyAssesments_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurverySections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurverySections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyInspectionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurverySectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DetailItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Defects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeesurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockUnitCost = table.Column<float>(type: "real", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    StockRemainingQuantity = table.Column<int>(type: "int", nullable: false),
                    StockReplaceYear = table.Column<int>(type: "int", nullable: false),
                    StockYearBand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockLifecycle = table.Column<int>(type: "int", nullable: false),
                    PropertyAssesmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyInspectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyInspectionItems_DetailItems_DetailItemId",
                        column: x => x.DetailItemId,
                        principalTable: "DetailItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyInspectionItems_PropertyAssesments_PropertyAssesmentId",
                        column: x => x.PropertyAssesmentId,
                        principalTable: "PropertyAssesments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyInspectionItems_SurverySections_SurverySectionId",
                        column: x => x.SurverySectionId,
                        principalTable: "SurverySections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyInspectionItems_UnitOfMeasurements_UnitOfMeasurementId",
                        column: x => x.UnitOfMeasurementId,
                        principalTable: "UnitOfMeasurements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PropertyInspectionItemImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyInspectionItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PropertyAssesmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyInspectionItemImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyInspectionItemImage_PropertyAssesments_PropertyAssesmentId",
                        column: x => x.PropertyAssesmentId,
                        principalTable: "PropertyAssesments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyInspectionItemImage_PropertyInspectionItems_PropertyInspectionItemId",
                        column: x => x.PropertyInspectionItemId,
                        principalTable: "PropertyInspectionItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAssesments_PropertyId",
                table: "PropertyAssesments",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInspectionItemImage_PropertyAssesmentId",
                table: "PropertyInspectionItemImage",
                column: "PropertyAssesmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInspectionItemImage_PropertyInspectionItemId",
                table: "PropertyInspectionItemImage",
                column: "PropertyInspectionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInspectionItems_DetailItemId",
                table: "PropertyInspectionItems",
                column: "DetailItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInspectionItems_PropertyAssesmentId",
                table: "PropertyInspectionItems",
                column: "PropertyAssesmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInspectionItems_SurverySectionId",
                table: "PropertyInspectionItems",
                column: "SurverySectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInspectionItems_UnitOfMeasurementId",
                table: "PropertyInspectionItems",
                column: "UnitOfMeasurementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyInspectionItemImage");

            migrationBuilder.DropTable(
                name: "PropertyInspectionItems");

            migrationBuilder.DropTable(
                name: "DetailItems");

            migrationBuilder.DropTable(
                name: "PropertyAssesments");

            migrationBuilder.DropTable(
                name: "SurverySections");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurements");
        }
    }
}
