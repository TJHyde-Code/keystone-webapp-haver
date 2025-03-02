using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class UpdatedNavprops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HaverGantts_Vendors_VendorId",
                table: "HaverGantts");

            migrationBuilder.RenameColumn(
                name: "VendorId",
                table: "HaverGantts",
                newName: "VendorID");

            migrationBuilder.RenameIndex(
                name: "IX_HaverGantts_VendorId",
                table: "HaverGantts",
                newName: "IX_HaverGantts_VendorID");

            migrationBuilder.AddForeignKey(
                name: "FK_HaverGantts_Vendors_VendorID",
                table: "HaverGantts",
                column: "VendorID",
                principalTable: "Vendors",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HaverGantts_Vendors_VendorID",
                table: "HaverGantts");

            migrationBuilder.RenameColumn(
                name: "VendorID",
                table: "HaverGantts",
                newName: "VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_HaverGantts_VendorID",
                table: "HaverGantts",
                newName: "IX_HaverGantts_VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_HaverGantts_Vendors_VendorId",
                table: "HaverGantts",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "ID");
        }
    }
}
