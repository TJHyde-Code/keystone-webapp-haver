using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class DataModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirSeal",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "BaseFrame",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "CoatingLining",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "Disassebmly",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "InstalledMedia",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "SparePartsSpareMedia",
                table: "OperationsSchedules");

            migrationBuilder.AddColumn<string>(
                name: "VendorAddress",
                table: "Vendors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VendorContactName",
                table: "Vendors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VendorEmail",
                table: "Vendors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VendorPhone",
                table: "Vendors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerContactName",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VendorAddress",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "VendorContactName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "VendorEmail",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "VendorPhone",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CustomerAddress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerContactName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Customers");

            migrationBuilder.AddColumn<bool>(
                name: "AirSeal",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BaseFrame",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CoatingLining",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Disassebmly",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InstalledMedia",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SparePartsSpareMedia",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
