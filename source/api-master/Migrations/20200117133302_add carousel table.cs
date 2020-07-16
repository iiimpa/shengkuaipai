using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class addcarouseltable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsAgent",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalRecharge",
                table: "Users",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Carousels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carousels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carousels");

            migrationBuilder.DropColumn(
                name: "IsAgent",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalRecharge",
                table: "Users");
        }
    }
}
