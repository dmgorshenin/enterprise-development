﻿// <auto-generated />
using System;
using DispatchService.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DispatchService.Model.Migrations
{
    [DbContext(typeof(DispatchServiceDbContext))]
    [Migration("20241031184455_Model")]
    partial class Model
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DispatchService.Model.Entities.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("address");

                    b.Property<string>("DriverLicense")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("driver_license");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("fullname");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("passport");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("phone_number");

                    b.HasKey("Id");

                    b.ToTable("driver");
                });

            modelBuilder.Entity("DispatchService.Model.Entities.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_time");

                    b.Property<string>("RouteNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("route_number");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_time");

                    b.Property<int>("assigned_driver")
                        .HasColumnType("integer");

                    b.Property<int>("assigned_transport")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("assigned_driver");

                    b.HasIndex("assigned_transport");

                    b.ToTable("route");
                });

            modelBuilder.Entity("DispatchService.Model.Entities.Transport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsLowFloor")
                        .HasColumnType("boolean")
                        .HasColumnName("is_low_floor");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("license_plate");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("integer")
                        .HasColumnName("max_capacity");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("model_name");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("vehicle_type");

                    b.Property<int>("YearOfManufacture")
                        .HasColumnType("integer")
                        .HasColumnName("year_of_manufacture");

                    b.HasKey("Id");

                    b.ToTable("transport");
                });

            modelBuilder.Entity("DispatchService.Model.Entities.Route", b =>
                {
                    b.HasOne("DispatchService.Model.Entities.Driver", "AssignedDriver")
                        .WithMany()
                        .HasForeignKey("assigned_driver")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DispatchService.Model.Entities.Transport", "AssignedTransport")
                        .WithMany()
                        .HasForeignKey("assigned_transport")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedDriver");

                    b.Navigation("AssignedTransport");
                });
#pragma warning restore 612, 618
        }
    }
}
