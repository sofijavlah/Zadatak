﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zadatak.Models;

namespace Zadatak.Migrations
{
    [DbContext(typeof(WorkContext))]
    partial class WorkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Zadatak.Models.Device", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("EmployeeId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Zadatak.Models.DeviceUsage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("DeviceId");

                    b.Property<long?>("EmployeeId");

                    b.Property<DateTime>("From");

                    b.Property<DateTime?>("To");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("DeviceUsages");
                });

            modelBuilder.Entity("Zadatak.Models.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<long>("OfficeId");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Zadatak.Models.Office", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("Zadatak.Models.Device", b =>
                {
                    b.HasOne("Zadatak.Models.Employee", "Employee")
                        .WithMany("Devices")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zadatak.Models.DeviceUsage", b =>
                {
                    b.HasOne("Zadatak.Models.Device", "Device")
                        .WithMany("UsageList")
                        .HasForeignKey("DeviceId");

                    b.HasOne("Zadatak.Models.Employee", "Employee")
                        .WithMany("UsageList")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("Zadatak.Models.Employee", b =>
                {
                    b.HasOne("Zadatak.Models.Office", "Office")
                        .WithMany("Employees")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
