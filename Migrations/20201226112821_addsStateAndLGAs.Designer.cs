﻿// <auto-generated />
using System;
using HaloBiz.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HaloBiz.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201226112821_addsStateAndLGAs")]
    partial class addsStateAndLGAs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("HaloBiz.Model_.Branch", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("HeadId")
                        .HasColumnType("int");

                    b.Property<long?>("HeadId1")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("HeadId1");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("HaloBiz.Model_.Division", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long>("HeadId")
                        .HasColumnType("bigint");

                    b.Property<string>("MissionStatement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("HeadId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("HaloBiz.Model_.LGA", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("StateId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("LGAs");
                });

            modelBuilder.Entity("HaloBiz.Model_.Office", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("BranchId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long>("HeadId")
                        .HasColumnType("bigint");

                    b.Property<long>("LGAId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("StateId")
                        .HasColumnType("bigint");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("HeadId");

                    b.HasIndex("LGAId");

                    b.HasIndex("StateId");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("HaloBiz.Model_.OperatingEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long>("DivisionId")
                        .HasColumnType("bigint");

                    b.Property<long>("HeadId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.HasIndex("HeadId");

                    b.ToTable("OperatingEntities");
                });

            modelBuilder.Entity("HaloBiz.Model_.ServiceCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("ServiceGroupId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("ServiceGroupId");

                    b.ToTable("ServiceCategories");
                });

            modelBuilder.Entity("HaloBiz.Model_.ServiceGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("OperatingEntityId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("OperatingEntityId");

                    b.ToTable("ServiceGroups");
                });

            modelBuilder.Entity("HaloBiz.Model_.Services", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<double>("BillableAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long>("ServiceCategoryId")
                        .HasColumnType("bigint");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<double>("VAT")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ServiceCategoryId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("HaloBiz.Model_.State", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Capital")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slogan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("HaloBiz.Model_.StrategicBusinessUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("OperatingEntityId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("OperatingEntityId");

                    b.ToTable("StrategicBusinessUnits");
                });

            modelBuilder.Entity("HaloBiz.Model_.UserProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AltEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltMobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacebookHandle")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("InstagramHandle")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkedInHandle")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProfileStatus")
                        .HasColumnType("bit");

                    b.Property<long>("StaffId")
                        .HasColumnType("bigint");

                    b.Property<string>("TwitterHandle")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("HaloBiz.Model_.Branch", b =>
                {
                    b.HasOne("HaloBiz.Model_.UserProfile", "Head")
                        .WithMany()
                        .HasForeignKey("HeadId1");

                    b.Navigation("Head");
                });

            modelBuilder.Entity("HaloBiz.Model_.Division", b =>
                {
                    b.HasOne("HaloBiz.Model_.UserProfile", "Head")
                        .WithMany()
                        .HasForeignKey("HeadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Head");
                });

            modelBuilder.Entity("HaloBiz.Model_.LGA", b =>
                {
                    b.HasOne("HaloBiz.Model_.State", "State")
                        .WithMany("LGAs")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("HaloBiz.Model_.Office", b =>
                {
                    b.HasOne("HaloBiz.Model_.Branch", "Branch")
                        .WithMany("Offices")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HaloBiz.Model_.UserProfile", "Head")
                        .WithMany()
                        .HasForeignKey("HeadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HaloBiz.Model_.LGA", "LGA")
                        .WithMany()
                        .HasForeignKey("LGAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HaloBiz.Model_.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Head");

                    b.Navigation("LGA");

                    b.Navigation("State");
                });

            modelBuilder.Entity("HaloBiz.Model_.OperatingEntity", b =>
                {
                    b.HasOne("HaloBiz.Model_.Division", "Division")
                        .WithMany("OperatingEntities")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HaloBiz.Model_.UserProfile", "Head")
                        .WithMany()
                        .HasForeignKey("HeadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Division");

                    b.Navigation("Head");
                });

            modelBuilder.Entity("HaloBiz.Model_.ServiceCategory", b =>
                {
                    b.HasOne("HaloBiz.Model_.ServiceGroup", "ServiceGroup")
                        .WithMany("ServiceCategories")
                        .HasForeignKey("ServiceGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceGroup");
                });

            modelBuilder.Entity("HaloBiz.Model_.ServiceGroup", b =>
                {
                    b.HasOne("HaloBiz.Model_.OperatingEntity", "OperatingEntity")
                        .WithMany("ServiceGroups")
                        .HasForeignKey("OperatingEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OperatingEntity");
                });

            modelBuilder.Entity("HaloBiz.Model_.Services", b =>
                {
                    b.HasOne("HaloBiz.Model_.ServiceCategory", "ServiceCategory")
                        .WithMany("Services")
                        .HasForeignKey("ServiceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceCategory");
                });

            modelBuilder.Entity("HaloBiz.Model_.StrategicBusinessUnit", b =>
                {
                    b.HasOne("HaloBiz.Model_.OperatingEntity", "OperatingEntity")
                        .WithMany("StrategicBusinessUnits")
                        .HasForeignKey("OperatingEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OperatingEntity");
                });

            modelBuilder.Entity("HaloBiz.Model_.Branch", b =>
                {
                    b.Navigation("Offices");
                });

            modelBuilder.Entity("HaloBiz.Model_.Division", b =>
                {
                    b.Navigation("OperatingEntities");
                });

            modelBuilder.Entity("HaloBiz.Model_.OperatingEntity", b =>
                {
                    b.Navigation("ServiceGroups");

                    b.Navigation("StrategicBusinessUnits");
                });

            modelBuilder.Entity("HaloBiz.Model_.ServiceCategory", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("HaloBiz.Model_.ServiceGroup", b =>
                {
                    b.Navigation("ServiceCategories");
                });

            modelBuilder.Entity("HaloBiz.Model_.State", b =>
                {
                    b.Navigation("LGAs");
                });
#pragma warning restore 612, 618
        }
    }
}
