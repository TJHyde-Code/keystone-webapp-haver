using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendorName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OperationsSchedules",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalesOrdNum = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtSalesOrdNum = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerID = table.Column<int>(type: "INTEGER", nullable: true),
                    MachineDesc = table.Column<string>(type: "TEXT", nullable: false),
                    SerialNum = table.Column<string>(type: "TEXT", nullable: false),
                    PackageReleaseName = table.Column<string>(type: "TEXT", nullable: false),
                    KickoffMeeting = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ReleaseApprovalDrawing = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    VendorID = table.Column<int>(type: "INTEGER", nullable: true),
                    PONum = table.Column<int>(type: "INTEGER", nullable: false),
                    PODueDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DeliveryDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    InstalledMedia = table.Column<bool>(type: "INTEGER", nullable: false),
                    SparePartsSpareMedia = table.Column<bool>(type: "INTEGER", nullable: false),
                    BaseFrame = table.Column<bool>(type: "INTEGER", nullable: false),
                    AirSeal = table.Column<bool>(type: "INTEGER", nullable: false),
                    CoatingLining = table.Column<bool>(type: "INTEGER", nullable: false),
                    Disassebmly = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsSchedules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OperationsSchedules_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OperationsSchedules_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationsSchedules_CustomerID",
                table: "OperationsSchedules",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsSchedules_VendorID",
                table: "OperationsSchedules",
                column: "VendorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationsSchedules");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
