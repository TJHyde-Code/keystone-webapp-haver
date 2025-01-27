using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class OperationsScheduleVendor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_OperationsScheduleVendors_OperationsScheduleID",
                table: "OperationsScheduleVendors",
                column: "OperationsScheduleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationsScheduleVendors");
        }
    }
}
