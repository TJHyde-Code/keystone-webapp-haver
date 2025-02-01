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
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerAddress = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CustomerContactName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CustomerEmail = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CustomerPhone = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Engineers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EngFirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EngMiddleName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    EngLastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EngPhone = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    EngEmail = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engineers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MachineDescriptions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Size = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    Class = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    Deck = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    NamePlateOrdered = table.Column<bool>(type: "INTEGER", nullable: false),
                    NameplateRecieved = table.Column<bool>(type: "INTEGER", nullable: false),
                    InstalledMedia = table.Column<bool>(type: "INTEGER", nullable: false),
                    SparePartsSpareMedia = table.Column<bool>(type: "INTEGER", nullable: false),
                    BaseFrame = table.Column<bool>(type: "INTEGER", nullable: false),
                    AirSeal = table.Column<bool>(type: "INTEGER", nullable: false),
                    CoatingLining = table.Column<bool>(type: "INTEGER", nullable: false),
                    Disassembly = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineDescriptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendorName = table.Column<string>(type: "TEXT", nullable: false),
                    VendorAddress = table.Column<string>(type: "TEXT", nullable: true),
                    VendorContactName = table.Column<string>(type: "TEXT", nullable: true),
                    VendorPhone = table.Column<string>(type: "TEXT", nullable: true),
                    VendorEmail = table.Column<string>(type: "TEXT", nullable: true)
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
                    MachineDesc = table.Column<string>(type: "TEXT", nullable: true),
                    SerialNum = table.Column<string>(type: "TEXT", nullable: true),
                    PackageReleaseName = table.Column<string>(type: "TEXT", nullable: true),
                    KickoffMeeting = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ReleaseApprovalDrawing = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    VendorID = table.Column<int>(type: "INTEGER", nullable: true),
                    PONum = table.Column<string>(type: "TEXT", nullable: true),
                    ProductionOrderNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PODueDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DeliveryDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EngineerID = table.Column<int>(type: "INTEGER", nullable: false),
                    MachineDescriptionID = table.Column<int>(type: "INTEGER", nullable: false)
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
                        name: "FK_OperationsSchedules_Engineers_EngineerID",
                        column: x => x.EngineerID,
                        principalTable: "Engineers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationsSchedules_MachineDescriptions_MachineDescriptionID",
                        column: x => x.MachineDescriptionID,
                        principalTable: "MachineDescriptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationsSchedules_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PreOrder = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Scope = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BudgetAssembHrs = table.Column<string>(type: "TEXT", nullable: false),
                    ActualAssembHours = table.Column<int>(type: "INTEGER", nullable: false),
                    ActualReworkHours = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherComments = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    OperationsScheduleID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notes_OperationsSchedules_OperationsScheduleID",
                        column: x => x.OperationsScheduleID,
                        principalTable: "OperationsSchedules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationsScheduleVendors",
                columns: table => new
                {
                    OperationsScheduleID = table.Column<int>(type: "INTEGER", nullable: false),
                    VendorID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsScheduleVendors", x => new { x.VendorID, x.OperationsScheduleID });
                    table.ForeignKey(
                        name: "FK_OperationsScheduleVendors_OperationsSchedules_OperationsScheduleID",
                        column: x => x.OperationsScheduleID,
                        principalTable: "OperationsSchedules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationsScheduleVendors_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_OperationsScheduleID",
                table: "Notes",
                column: "OperationsScheduleID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationsSchedules_CustomerID",
                table: "OperationsSchedules",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsSchedules_EngineerID",
                table: "OperationsSchedules",
                column: "EngineerID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsSchedules_MachineDescriptionID",
                table: "OperationsSchedules",
                column: "MachineDescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsSchedules_VendorID",
                table: "OperationsSchedules",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsScheduleVendors_OperationsScheduleID",
                table: "OperationsScheduleVendors",
                column: "OperationsScheduleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "OperationsScheduleVendors");

            migrationBuilder.DropTable(
                name: "OperationsSchedules");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Engineers");

            migrationBuilder.DropTable(
                name: "MachineDescriptions");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
