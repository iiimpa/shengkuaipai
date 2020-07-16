using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Knowledges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Ip = table.Column<string>(nullable: true),
                    Success = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    Show = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderNo = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Platform = table.Column<int>(nullable: false),
                    PlanId = table.Column<int>(nullable: false),
                    Keyword = table.Column<string>(nullable: true),
                    Domain = table.Column<string>(nullable: true),
                    Days = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Times = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    Proxy = table.Column<string>(nullable: true),
                    Ua = table.Column<string>(nullable: true),
                    Resolution = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ScheduleTime = table.Column<DateTime>(nullable: false),
                    FinishTime = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Cell = table.Column<string>(nullable: true),
                    Coin = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Knowledges");

            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DropTable(
                name: "Notices");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
