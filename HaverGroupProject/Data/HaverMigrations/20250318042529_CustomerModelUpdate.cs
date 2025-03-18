using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class CustomerModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "CustomerContactName",
                table: "Customers",
                newName: "CustContactLastName");

            migrationBuilder.AddColumn<string>(
                name: "CustContactFirst",
                table: "Customers",
                type: "TEXT",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustContactFirst",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "CustContactLastName",
                table: "Customers",
                newName: "CustomerContactName");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
