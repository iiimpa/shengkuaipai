using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class up_task : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "mode",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "optimization",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mode",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "optimization",
                table: "Tasks");
        }
    }
}
