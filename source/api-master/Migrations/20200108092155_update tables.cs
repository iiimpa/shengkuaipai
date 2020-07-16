using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class updatetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RealCost",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RealCost",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Orders");
        }
    }
}
