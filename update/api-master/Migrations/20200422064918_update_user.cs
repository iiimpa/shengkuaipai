using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class update_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "alipay",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "cash_in",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "cash_out",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alipay",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "cash_in",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "cash_out",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Users");
        }
    }
}
