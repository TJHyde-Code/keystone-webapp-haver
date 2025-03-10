using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class AddMultipleMachines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationsScheduleMachines",
                columns: table => new
                {
                    OperationsScheduleID = table.Column<int>(type: "INTEGER", nullable: false),
                    MachineDescriptionID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsScheduleMachines", x => new { x.MachineDescriptionID, x.OperationsScheduleID });
                    table.ForeignKey(
                        name: "FK_OperationsScheduleMachines_MachineDescriptions_MachineDescriptionID",
                        column: x => x.MachineDescriptionID,
                        principalTable: "MachineDescriptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationsScheduleMachines_OperationsSchedules_OperationsScheduleID",
                        column: x => x.OperationsScheduleID,
                        principalTable: "OperationsSchedules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationsScheduleMachines_OperationsScheduleID",
                table: "OperationsScheduleMachines",
                column: "OperationsScheduleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationsScheduleMachines");
        }
    }
}
