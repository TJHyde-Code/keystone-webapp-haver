using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class Engineer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HaverGantts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PurchaseOrderNum = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PromiseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ApprvDwgRecvd = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Progress = table.Column<decimal>(type: "TEXT", nullable: false),
                    GanttNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    MachineDescriptionID = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomerID = table.Column<int>(type: "INTEGER", nullable: false),
                    EngineerID = table.Column<int>(type: "INTEGER", nullable: true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HaverGantts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HaverGantts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HaverGantts_Engineers_EngineerID",
                        column: x => x.EngineerID,
                        principalTable: "Engineers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HaverGantts_MachineDescriptions_MachineDescriptionID",
                        column: x => x.MachineDescriptionID,
                        principalTable: "MachineDescriptions",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_HaverGantts_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HaverGantts_CustomerID",
                table: "HaverGantts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_HaverGantts_EngineerID",
                table: "HaverGantts",
                column: "EngineerID");

            migrationBuilder.CreateIndex(
                name: "IX_HaverGantts_MachineDescriptionID",
                table: "HaverGantts",
                column: "MachineDescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_HaverGantts_VendorId",
                table: "HaverGantts",
                column: "VendorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HaverGantts");
        }
    }
}
