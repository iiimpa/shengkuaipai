using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class addnewtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alipay",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pid",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentRank",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RankTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StartRank",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Keyword = table.Column<string>(nullable: true),
                    Domain = table.Column<string>(nullable: true),
                    Origin = table.Column<int>(nullable: false),
                    Now = table.Column<int>(nullable: false),
                    Up = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClickPlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    T0 = table.Column<int>(nullable: false),
                    T1 = table.Column<int>(nullable: false),
                    T2 = table.Column<int>(nullable: false),
                    T3 = table.Column<int>(nullable: false),
                    T4 = table.Column<int>(nullable: false),
                    T5 = table.Column<int>(nullable: false),
                    T6 = table.Column<int>(nullable: false),
                    T7 = table.Column<int>(nullable: false),
                    T8 = table.Column<int>(nullable: false),
                    T9 = table.Column<int>(nullable: false),
                    T10 = table.Column<int>(nullable: false),
                    T11 = table.Column<int>(nullable: false),
                    T12 = table.Column<int>(nullable: false),
                    T13 = table.Column<int>(nullable: false),
                    T14 = table.Column<int>(nullable: false),
                    T15 = table.Column<int>(nullable: false),
                    T16 = table.Column<int>(nullable: false),
                    T17 = table.Column<int>(nullable: false),
                    T18 = table.Column<int>(nullable: false),
                    T19 = table.Column<int>(nullable: false),
                    T20 = table.Column<int>(nullable: false),
                    T21 = table.Column<int>(nullable: false),
                    T22 = table.Column<int>(nullable: false),
                    T23 = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClickPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descirption = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "varchar(10)", nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    RelationId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "ClickPlans");

            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropColumn(
                name: "Alipay",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Pid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentRank",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RankTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StartRank",
                table: "Orders");
        }
    }
}
