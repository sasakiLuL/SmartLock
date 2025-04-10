﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SmartLock.DataAccessLayer;

#nullable disable

namespace SmartLock.DataAccessLayer.Migrations
{
    [DbContext(typeof(SmartLockContext))]
    [Migration("20250222190224_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SmartLock.Domain.Actions.ActionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CommandType")
                        .HasColumnType("integer");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ActionModels");
                });

            modelBuilder.Entity("SmartLock.Domain.Devices.DeviceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DeviceName")
                        .HasColumnType("text");

                    b.Property<int>("DeviceStatus")
                        .HasColumnType("integer");

                    b.Property<Guid>("HardwareId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RegisteredOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("DeviceModels");
                });

            modelBuilder.Entity("SmartLock.Domain.Users.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("IdentityProviderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("UserModels");
                });

            modelBuilder.Entity("SmartLock.Domain.Actions.ActionModel", b =>
                {
                    b.HasOne("SmartLock.Domain.Devices.DeviceModel", null)
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartLock.Domain.Users.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SmartLock.Domain.Devices.DeviceModel", b =>
                {
                    b.HasOne("SmartLock.Domain.Users.UserModel", null)
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
