using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class tradeadddetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Trades",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Coin",
                table: "Trades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TradeNo",
                table: "Trades",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coin",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "TradeNo",
                table: "Trades");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Trades",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
