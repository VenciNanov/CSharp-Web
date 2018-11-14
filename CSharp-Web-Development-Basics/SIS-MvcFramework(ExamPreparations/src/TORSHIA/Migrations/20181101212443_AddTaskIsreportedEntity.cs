using Microsoft.EntityFrameworkCore.Migrations;

namespace TORSHIA.Migrations
{
    public partial class AddTaskIsreportedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "Tasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "Tasks");
        }
    }
}
