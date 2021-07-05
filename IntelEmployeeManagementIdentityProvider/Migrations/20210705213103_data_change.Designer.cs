﻿// <auto-generated />
using System;
using Intel.EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Intel.EmployeeManagement.IdentityProvider.Migrations
{
    [DbContext(typeof(EmployeeDbContext))]
    [Migration("20210705213103_data_change")]
    partial class data_change
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Intel.EmployeeManagement.Data.Entities.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DateOfBirth = new DateTime(1980, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = 5,
                            FirstName = "John",
                            Gender = "M",
                            LastName = "Smith",
                            MiddleName = "",
                            Position = "Sales Rpresentative"
                        },
                        new
                        {
                            ID = 2,
                            DateOfBirth = new DateTime(1985, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = 10,
                            FirstName = "Jack",
                            Gender = "M",
                            LastName = "Jackson",
                            MiddleName = "",
                            Position = "Operations Manager"
                        },
                        new
                        {
                            ID = 3,
                            DateOfBirth = new DateTime(1987, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = 15,
                            FirstName = "Jane",
                            Gender = "F",
                            LastName = "Ferguson",
                            MiddleName = "Willa",
                            Position = "Operations Manager"
                        },
                        new
                        {
                            ID = 4,
                            DateOfBirth = new DateTime(1978, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = 20,
                            FirstName = "Leo",
                            Gender = "M",
                            LastName = "Wilson",
                            MiddleName = "",
                            Position = "Head Chef"
                        },
                        new
                        {
                            ID = 5,
                            DateOfBirth = new DateTime(1974, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = 15,
                            FirstName = "Harry",
                            Gender = "M",
                            LastName = "Burton",
                            MiddleName = "",
                            Position = "CEO"
                        });
                });

            modelBuilder.Entity("Intel.EmployeeManagement.Data.Entities.Log", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExceptionMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Intel.EmployeeManagement.Data.Entities.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            RoleName = "hr",
                            UserID = 1
                        },
                        new
                        {
                            ID = 2,
                            RoleName = "hr",
                            UserID = 2
                        },
                        new
                        {
                            ID = 3,
                            RoleName = "data entry",
                            UserID = 3
                        },
                        new
                        {
                            ID = 4,
                            RoleName = "admin",
                            UserID = 1
                        });
                });

            modelBuilder.Entity("Intel.EmployeeManagement.Data.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "bikash@gmail.com",
                            Password = "password",
                            Username = "bikash@gmail.com"
                        },
                        new
                        {
                            ID = 2,
                            Email = "jack@gmail.com",
                            Password = "password",
                            Username = "jack@gmail.com"
                        },
                        new
                        {
                            ID = 3,
                            Email = "mike@gmail.com",
                            Password = "password",
                            Username = "mike@gmail.com"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
