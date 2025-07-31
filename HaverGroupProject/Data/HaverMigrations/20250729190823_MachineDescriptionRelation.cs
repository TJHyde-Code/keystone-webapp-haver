using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class MachineDescriptionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsSchedules_MachineDescriptions_MachineDescriptionID",
                table: "OperationsSchedules");

            migrationBuilder.AddColumn<int>(
                name: "MachineDescriptionID1",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationsSchedules_MachineDescriptionID1",
                table: "OperationsSchedules",
                column: "MachineDescriptionID1");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsSchedules_MachineDescriptions_MachineDescriptionID",
                table: "OperationsSchedules",
                column: "MachineDescriptionID",
                principalTable: "MachineDescriptions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsSchedules_MachineDescriptions_MachineDescriptionID1",
                table: "OperationsSchedules",
                column: "MachineDescriptionID1",
                principalTable: "MachineDescriptions",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsSchedules_MachineDescriptions_MachineDescriptionID",
                table: "OperationsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsSchedules_MachineDescriptions_MachineDescriptionID1",
                table: "OperationsSchedules");

            migrationBuilder.DropIndex(
                name: "IX_OperationsSchedules_MachineDescriptionID1",
                table: "OperationsSchedules");

            migrationBuilder.DropColumn(
                name: "MachineDescriptionID1",
                table: "OperationsSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsSchedules_MachineDescriptions_MachineDescriptionID",
                table: "OperationsSchedules",
                column: "MachineDescriptionID",
                principalTable: "MachineDescriptions",
                principalColumn: "ID");
        }
    }
}
