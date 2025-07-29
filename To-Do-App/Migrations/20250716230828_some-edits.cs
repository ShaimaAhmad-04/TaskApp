using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_Do_App.Migrations
{
    /// <inheritdoc />
    public partial class someedits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Lookups_lookupId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_lookupId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "lookupId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "priorityId",
                table: "Tasks",
                newName: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PriorityId",
                table: "Tasks",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Lookups_PriorityId",
                table: "Tasks",
                column: "PriorityId",
                principalTable: "Lookups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Lookups_PriorityId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_PriorityId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "PriorityId",
                table: "Tasks",
                newName: "priorityId");

            migrationBuilder.AddColumn<long>(
                name: "lookupId",
                table: "Tasks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_lookupId",
                table: "Tasks",
                column: "lookupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Lookups_lookupId",
                table: "Tasks",
                column: "lookupId",
                principalTable: "Lookups",
                principalColumn: "Id");
        }
    }
}
