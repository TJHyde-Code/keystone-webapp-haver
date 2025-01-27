using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class FixingProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsScheduleVendors_Vendors_VendorID",
                table: "OperationsScheduleVendors");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsScheduleVendors_Vendors_VendorID",
                table: "OperationsScheduleVendors",
                column: "VendorID",
                principalTable: "Vendors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsScheduleVendors_Vendors_VendorID",
                table: "OperationsScheduleVendors");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsScheduleVendors_Vendors_VendorID",
                table: "OperationsScheduleVendors",
                column: "VendorID",
                principalTable: "Vendors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
