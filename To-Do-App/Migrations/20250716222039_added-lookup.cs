using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace To_Do_App.Migrations
{
    /// <inheritdoc />
    public partial class addedlookup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "priority",
                table: "Tasks");

            migrationBuilder.AddColumn<long>(
                name: "lookupId",
                table: "Tasks",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "priorityId",
                table: "Tasks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lookups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorCode = table.Column<int>(type: "int", nullable: false),
                    MinorCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookups", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "MajorCode", "MinorCode", "Name" },
                values: new object[,]
                {
                    { 1L, 0, 0, "Priority" },
                    { 2L, 0, 1, "High" },
                    { 3L, 0, 2, "Medium" },
                    { 4L, 0, 3, "Low" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Lookups_lookupId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Lookups");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_lookupId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "lookupId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "priorityId",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "priority",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
