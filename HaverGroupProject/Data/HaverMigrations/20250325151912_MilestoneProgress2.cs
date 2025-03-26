using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class MilestoneProgress2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProgressApprobalDrawing",
                table: "OperationsSchedules",
                newName: "ProgressApprovalDrawing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProgressApprovalDrawing",
                table: "OperationsSchedules",
                newName: "ProgressApprobalDrawing");
        }
    }
}
