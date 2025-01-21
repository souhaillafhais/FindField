﻿// <auto-generated />
using System;
using GestionTerrains.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionTerrains.Migrations
{
    [DbContext(typeof(ReservationDbContext))]
    [Migration("20241216195918_AddTerrainStaffAndReservationRelations")]
    partial class AddTerrainStaffAndReservationRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestionTerrains.Models.Domain.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("HeureDebut")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HeureFin")
                        .HasColumnType("time");

                    b.Property<string>("NomClient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TerrainId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TerrainId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("GestionTerrains.Models.Domain.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("GestionTerrains.Models.Domain.Terrain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Disponible")
                        .HasColumnType("bit");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PrixParHeure")
                        .HasColumnType("real");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StaffId");

                    b.ToTable("Terrains");
                });

            modelBuilder.Entity("GestionTerrains.Models.Domain.Reservation", b =>
                {
                    b.HasOne("GestionTerrains.Models.Domain.Terrain", "Terrain")
                        .WithMany()
                        .HasForeignKey("TerrainId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Terrain");
                });

            modelBuilder.Entity("GestionTerrains.Models.Domain.Terrain", b =>
                {
                    b.HasOne("GestionTerrains.Models.Domain.Staff", "Responsable")
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Responsable");
                });
#pragma warning restore 612, 618
        }
    }
}
