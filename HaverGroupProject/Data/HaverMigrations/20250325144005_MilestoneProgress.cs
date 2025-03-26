using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class MilestoneProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ProgressApprobalDrawing",
                table: "OperationsSchedules",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProgressEngineerPackage",
                table: "OperationsSchedules",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProgressPreOrder",
                table: "OperationsSchedules",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProgressPurchaseOrder",
                table: "OperationsSchedules",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProgressReadinesstoShip",
                table: "OperationsSchedules",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgressApprobalDrawing",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "ProgressEngineerPackage",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "ProgressPreOrder",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "ProgressPurchaseOrder",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "ProgressReadinesstoShip",
                table: "OperationsSchedules");
        }
    }
}
