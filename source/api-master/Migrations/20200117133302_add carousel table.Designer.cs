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
    [Migration("20200117133302_add carousel table")]
    partial class addcarouseltable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebApi.Models.Carousel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Link")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Carousels");
                });

            modelBuilder.Entity("WebApi.Models.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Date")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Domain")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Keyword")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Now")
                        .HasColumnType("int");

                    b.Property<int>("Origin")
                        .HasColumnType("int");

                    b.Property<int>("Up")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("WebApi.Models.ClickPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("T0")
                        .HasColumnType("int");

                    b.Property<int>("T1")
                        .HasColumnType("int");

                    b.Property<int>("T10")
                        .HasColumnType("int");

                    b.Property<int>("T11")
                        .HasColumnType("int");

                    b.Property<int>("T12")
                        .HasColumnType("int");

                    b.Property<int>("T13")
                        .HasColumnType("int");

                    b.Property<int>("T14")
                        .HasColumnType("int");

                    b.Property<int>("T15")
                        .HasColumnType("int");

                    b.Property<int>("T16")
                        .HasColumnType("int");

                    b.Property<int>("T17")
                        .HasColumnType("int");

                    b.Property<int>("T18")
                        .HasColumnType("int");

                    b.Property<int>("T19")
                        .HasColumnType("int");

                    b.Property<int>("T2")
                        .HasColumnType("int");

                    b.Property<int>("T20")
                        .HasColumnType("int");

                    b.Property<int>("T21")
                        .HasColumnType("int");

                    b.Property<int>("T22")
                        .HasColumnType("int");

                    b.Property<int>("T23")
                        .HasColumnType("int");

                    b.Property<int>("T3")
                        .HasColumnType("int");

                    b.Property<int>("T4")
                        .HasColumnType("int");

                    b.Property<int>("T5")
                        .HasColumnType("int");

                    b.Property<int>("T6")
                        .HasColumnType("int");

                    b.Property<int>("T7")
                        .HasColumnType("int");

                    b.Property<int>("T8")
                        .HasColumnType("int");

                    b.Property<int>("T9")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ClickPlans");
                });

            modelBuilder.Entity("WebApi.Models.Config", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descirption")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Key")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("WebApi.Models.Knowledge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Knowledges");
                });

            modelBuilder.Entity("WebApi.Models.LoginLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Ip")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Success")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LoginLogs");
                });

            modelBuilder.Entity("WebApi.Models.Notice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Show")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("WebApi.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrentRank")
                        .HasColumnType("int");

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<string>("Domain")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Keyword")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OrderNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("Platform")
                        .HasColumnType("int");

                    b.Property<DateTime>("RankTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("StartRank")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApi.Models.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Times")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("WebApi.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("WebApi.Models.RechargePlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("GiftCoin")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RealCoin")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("RechargePlans");
                });

            modelBuilder.Entity("WebApi.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FinishTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("Proxy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RealCost")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RequestTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Resolution")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ScheduleTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Ua")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("WebApi.Models.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RelationId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("WebApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Account")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Alipay")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Cell")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Coin")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IsAgent")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Pid")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalRecharge")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
