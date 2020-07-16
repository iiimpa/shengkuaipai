using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class update_plan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jump_times",
                table: "ResidencePlans");

            migrationBuilder.DropColumn(
                name: "time",
                table: "ResidencePlans");

            migrationBuilder.DropColumn(
                name: "plan_content",
                table: "ClickPlans");

            migrationBuilder.AddColumn<string>(
                name: "randomJumpCount",
                table: "ResidencePlans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "randomWaitCount",
                table: "ResidencePlans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "create_status",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "task_create_time",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "clickLimtCount",
                table: "ClickPlans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "times",
                table: "ClickPlans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "randomJumpCount",
                table: "ResidencePlans");

            migrationBuilder.DropColumn(
                name: "randomWaitCount",
                table: "ResidencePlans");

            migrationBuilder.DropColumn(
                name: "create_status",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "task_create_time",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "clickLimtCount",
                table: "ClickPlans");

            migrationBuilder.DropColumn(
                name: "times",
                table: "ClickPlans");

            migrationBuilder.AddColumn<int>(
                name: "Jump_times",
                table: "ResidencePlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "time",
                table: "ResidencePlans",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "plan_content",
                table: "ClickPlans",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
