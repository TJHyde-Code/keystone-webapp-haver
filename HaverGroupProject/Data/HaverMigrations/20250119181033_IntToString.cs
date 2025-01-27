using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class IntToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsSchedules_Vendors_VendorID",
                table: "OperationsSchedules");

            migrationBuilder.AlterColumn<string>(
                name: "PONum",
                table: "OperationsSchedules",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsSchedules_Vendors_VendorID",
                table: "OperationsSchedules",
                column: "VendorID",
                principalTable: "Vendors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsSchedules_Vendors_VendorID",
                table: "OperationsSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "PONum",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsSchedules_Vendors_VendorID",
                table: "OperationsSchedules",
                column: "VendorID",
                principalTable: "Vendors",
                principalColumn: "ID");
        }
    }
}
