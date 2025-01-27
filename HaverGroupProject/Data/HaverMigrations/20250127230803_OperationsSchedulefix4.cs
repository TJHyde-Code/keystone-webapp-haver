using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class OperationsSchedulefix4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsSchedules_Engineers_EngineerID",
                table: "OperationsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsSchedules_MachineDescriptions_MachineDescriptionID",
                table: "OperationsSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Notes_OperationsScheduleID",
                table: "Notes");

            migrationBuilder.AlterColumn<int>(
                name: "MachineDescriptionID",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "EngineerID",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_OperationsScheduleID",
                table: "Notes",
                column: "OperationsScheduleID");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsSchedules_Engineers_EngineerID",
                table: "OperationsSchedules",
                column: "EngineerID",
                principalTable: "Engineers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsSchedules_MachineDescriptions_MachineDescriptionID",
                table: "OperationsSchedules",
                column: "MachineDescriptionID",
                principalTable: "MachineDescriptions",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsSchedules_Engineers_EngineerID",
                table: "OperationsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsSchedules_MachineDescriptions_MachineDescriptionID",
                table: "OperationsSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Notes_OperationsScheduleID",
                table: "Notes");

            migrationBuilder.AlterColumn<int>(
                name: "MachineDescriptionID",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EngineerID",
                table: "OperationsSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_OperationsScheduleID",
                table: "Notes",
                column: "OperationsScheduleID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsSchedules_Engineers_EngineerID",
                table: "OperationsSchedules",
                column: "EngineerID",
                principalTable: "Engineers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsSchedules_MachineDescriptions_MachineDescriptionID",
                table: "OperationsSchedules",
                column: "MachineDescriptionID",
                principalTable: "MachineDescriptions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
