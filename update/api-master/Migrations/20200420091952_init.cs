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
                name: "Blacklists",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(nullable: false),
                    entry_time = table.Column<DateTime>(nullable: false),
                    entry_reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blacklists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ClickPlans",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    plan_name = table.Column<string>(nullable: true),
                    user_id = table.Column<int>(nullable: false),
                    create_time = table.Column<DateTime>(nullable: false),
                    plan_content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClickPlans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    group_name = table.Column<string>(nullable: true),
                    user_id = table.Column<int>(nullable: false),
                    cover = table.Column<string>(nullable: true),
                    details = table.Column<string>(nullable: true),
                    create_time = table.Column<DateTime>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LoginLogs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(nullable: false),
                    login_ip = table.Column<string>(nullable: true),
                    success = table.Column<bool>(nullable: false),
                    login_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(nullable: false),
                    serial_no = table.Column<string>(nullable: true),
                    create_time = table.Column<DateTime>(nullable: false),
                    platform = table.Column<int>(nullable: false),
                    keyword = table.Column<string>(nullable: true),
                    domain = table.Column<string>(nullable: true),
                    xiongzhang = table.Column<string>(nullable: true),
                    days = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false),
                    start_rank = table.Column<int>(nullable: false),
                    current_rank = table.Column<int>(nullable: false),
                    amount = table.Column<double>(nullable: false),
                    times = table.Column<string>(nullable: true),
                    clickLimtCount = table.Column<string>(nullable: true),
                    randomJumpCount = table.Column<string>(nullable: true),
                    randomWaitCount = table.Column<string>(nullable: true),
                    increase = table.Column<string>(nullable: true),
                    percentage = table.Column<string>(nullable: true),
                    group_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RechargePlans",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    amount = table.Column<double>(nullable: false),
                    real_coin = table.Column<double>(nullable: false),
                    gift_coin = table.Column<double>(nullable: false),
                    create_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargePlans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RecommendationPlans",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    user_id = table.Column<int>(nullable: false),
                    create_time = table.Column<DateTime>(nullable: false),
                    content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationPlans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ResidencePlans",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    time = table.Column<string>(nullable: true),
                    Jump_times = table.Column<int>(nullable: false),
                    price = table.Column<double>(nullable: false),
                    create_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidencePlans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    order_id = table.Column<int>(nullable: false),
                    domain = table.Column<string>(nullable: true),
                    keyword = table.Column<string>(nullable: true),
                    platform = table.Column<int>(nullable: false),
                    xiongzhang = table.Column<string>(nullable: true),
                    proxy_ip = table.Column<string>(nullable: true),
                    user_agent = table.Column<string>(nullable: true),
                    resolution = table.Column<string>(nullable: true),
                    cost = table.Column<double>(nullable: false),
                    realcost = table.Column<double>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    stay_time = table.Column<string>(nullable: true),
                    jump_times = table.Column<int>(nullable: false),
                    schedule_time = table.Column<DateTime>(nullable: false),
                    request_time = table.Column<DateTime>(nullable: false),
                    finish_time = table.Column<DateTime>(nullable: false),
                    create_time = table.Column<DateTime>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    trade_type = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    trade_no = table.Column<string>(nullable: true),
                    plan_id = table.Column<int>(nullable: false),
                    amount = table.Column<double>(nullable: false),
                    coin = table.Column<double>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    create_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nickname = table.Column<string>(nullable: true),
                    account = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    telephone = table.Column<string>(nullable: true),
                    roles = table.Column<string>(nullable: true),
                    level = table.Column<int>(nullable: false),
                    coin = table.Column<double>(nullable: false),
                    avatar = table.Column<string>(nullable: true),
                    parent_id = table.Column<int>(nullable: false),
                    url = table.Column<string>(nullable: true),
                    create_time = table.Column<DateTime>(nullable: false),
                    login_time = table.Column<DateTime>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false),
                    proxy_person_num = table.Column<int>(nullable: false),
                    discount = table.Column<double>(nullable: false),
                    in_blacklist = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blacklists");

            migrationBuilder.DropTable(
                name: "ClickPlans");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "RechargePlans");

            migrationBuilder.DropTable(
                name: "RecommendationPlans");

            migrationBuilder.DropTable(
                name: "ResidencePlans");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
