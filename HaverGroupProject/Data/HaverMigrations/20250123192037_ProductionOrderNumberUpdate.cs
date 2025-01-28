using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverGroupProject.Data.HaverMigrations
{
    /// <inheritdoc />
    public partial class ProductionOrderNumberUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PONum",
                table: "OperationsSchedules",
                newName: "ProductionOrderNumber");

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PreOrder = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Scope = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BudgetAssembHrs = table.Column<string>(type: "TEXT", nullable: false),
                    ActualAssembHours = table.Column<int>(type: "INTEGER", nullable: false),
                    ActualReworkHours = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherComments = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    OperationsScheduleID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notes_OperationsSchedules_OperationsScheduleID",
                        column: x => x.OperationsScheduleID,
                        principalTable: "OperationsSchedules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_OperationsScheduleID",
                table: "Notes",
                column: "OperationsScheduleID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.RenameColumn(
                name: "ProductionOrderNumber",
                table: "OperationsSchedules",
                newName: "PONum");
        }
    }
}
