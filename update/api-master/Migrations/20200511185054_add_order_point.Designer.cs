﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Models;

namespace WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200511185054_add_order_point")]
    partial class add_order_point
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebApi.Models.Blacklist", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("entry_reason")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("entry_time")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Blacklists");
                });

            modelBuilder.Entity("WebApi.Models.ClickPlan", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("clickLimtCount")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("create_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("plan_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("times")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("ClickPlans");
                });

            modelBuilder.Entity("WebApi.Models.Group", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("cover")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("create_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("details")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("group_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("update_time")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("WebApi.Models.LoginLog", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("login_ip")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("login_time")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("success")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("LoginLogs");
                });

            modelBuilder.Entity("WebApi.Models.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("amount")
                        .HasColumnType("double");

                    b.Property<string>("clickLimtCount")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("create_status")
                        .HasColumnType("int");

                    b.Property<DateTime>("create_time")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("current_rank")
                        .HasColumnType("int");

                    b.Property<int>("days")
                        .HasColumnType("int");

                    b.Property<string>("domain")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("group_id")
                        .HasColumnType("int");

                    b.Property<string>("increase")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("keyword")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("percentage")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("platform")
                        .HasColumnType("int");

                    b.Property<int>("point")
                        .HasColumnType("int");

                    b.Property<string>("randomJumpCount")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("randomWaitCount")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("serial_no")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("start_rank")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<DateTime>("task_create_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("times")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("update_time")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<string>("xiongzhang")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApi.Models.RechargePlan", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("amount")
                        .HasColumnType("double");

                    b.Property<DateTime>("create_time")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("gift_coin")
                        .HasColumnType("double");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("real_coin")
                        .HasColumnType("double");

                    b.Property<string>("url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.ToTable("RechargePlans");
                });

            modelBuilder.Entity("WebApi.Models.RecommendationPlan", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("create_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("RecommendationPlans");
                });

            modelBuilder.Entity("WebApi.Models.ResidenPlan", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("create_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("price")
                        .HasColumnType("double");

                    b.Property<string>("randomJumpCount")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("randomWaitCount")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("ResidencePlans");
                });

            modelBuilder.Entity("WebApi.Models.Task", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("cost")
                        .HasColumnType("double");

                    b.Property<DateTime>("create_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("domain")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("finish_time")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("jump_times")
                        .HasColumnType("int");

                    b.Property<string>("keyword")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("order_id")
                        .HasColumnType("int");

                    b.Property<int>("platform")
                        .HasColumnType("int");

                    b.Property<string>("proxy_ip")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("realcost")
                        .HasColumnType("double");

                    b.Property<DateTime>("request_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("resolution")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("schedule_time")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("stay_time")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("update_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("user_agent")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("xiongzhang")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("WebApi.Models.Trade", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("account")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("amount")
                        .HasColumnType("double");

                    b.Property<double>("coin")
                        .HasColumnType("double");

                    b.Property<DateTime>("create_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("plan_id")
                        .HasColumnType("int");

                    b.Property<string>("trade_no")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("trade_type")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("WebApi.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("account")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("alipay")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("cash_in")
                        .HasColumnType("double");

                    b.Property<double>("cash_out")
                        .HasColumnType("double");

                    b.Property<double>("coin")
                        .HasColumnType("double");

                    b.Property<DateTime>("create_time")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("discount")
                        .HasColumnType("double");

                    b.Property<string>("email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("in_blacklist")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("level")
                        .HasColumnType("int");

                    b.Property<DateTime>("login_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("nickname")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("parent_id")
                        .HasColumnType("int");

                    b.Property<string>("password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("price")
                        .HasColumnType("double");

                    b.Property<int>("proxy_person_num")
                        .HasColumnType("int");

                    b.Property<string>("roles")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("telephone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("update_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
