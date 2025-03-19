using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class OSModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseApprovalDrawing",
                table: "OperationsSchedules",
                newName: "ReadinessToShipActual");

            migrationBuilder.RenameColumn(
                name: "PackageReleaseName",
                table: "OperationsSchedules",
                newName: "PurchaseOrderDueDate");

            migrationBuilder.RenameColumn(
                name: "PODueDate",
                table: "OperationsSchedules",
                newName: "PreOrderReleased");

            migrationBuilder.RenameColumn(
                name: "ExtSalesOrdNum",
                table: "OperationsSchedules",
                newName: "QIComplete");

            migrationBuilder.RenameColumn(
                name: "DeliveryDate",
                table: "OperationsSchedules",
                newName: "PUrchaseOrderRecieved");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDrawingExpected",
                table: "OperationsSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDrawingReleased",
                table: "OperationsSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDrawingReturned",
                table: "OperationsSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EngineerPackageExpected",
                table: "OperationsSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EngineerPackageReleased",
                table: "OperationsSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NCRRaised",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PreOrderExpected",
                table: "OperationsSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseOrderExpected",
                table: "OperationsSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadinessToShipExpected",
                table: "OperationsSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "OperationsSchedules",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalDrawingExpected",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "ApprovalDrawingReleased",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "ApprovalDrawingReturned",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "EngineerPackageExpected",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "EngineerPackageReleased",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "NCRRaised",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "PreOrderExpected",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderExpected",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "ReadinessToShipExpected",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "OperationsSchedules");

            migrationBuilder.RenameColumn(
                name: "ReadinessToShipActual",
                table: "OperationsSchedules",
                newName: "ReleaseApprovalDrawing");

            migrationBuilder.RenameColumn(
                name: "QIComplete",
                table: "OperationsSchedules",
                newName: "ExtSalesOrdNum");

            migrationBuilder.RenameColumn(
                name: "PurchaseOrderDueDate",
                table: "OperationsSchedules",
                newName: "PackageReleaseName");

            migrationBuilder.RenameColumn(
                name: "PreOrderReleased",
                table: "OperationsSchedules",
                newName: "PODueDate");

            migrationBuilder.RenameColumn(
                name: "PUrchaseOrderRecieved",
                table: "OperationsSchedules",
                newName: "DeliveryDate");
        }
    }
}
