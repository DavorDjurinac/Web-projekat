﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Models;

namespace Server.Migrations
{
    [DbContext(typeof(ContextKlasa))]
    [Migration("20220111182715_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Server.Models.Hotel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Adresa")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Adresa");

                    b.Property<int>("BrojSobaPoTipu")
                        .HasColumnType("int")
                        .HasColumnName("BrojSobaPoTipu");

                    b.Property<string>("Naziv")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Naziv");

                    b.HasKey("ID");

                    b.ToTable("Hotel");
                });

            modelBuilder.Entity("Server.Models.Racun", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<int>("BrojDana")
                        .HasColumnType("int")
                        .HasColumnName("BrojDana");

                    b.Property<int>("BrojSobe")
                        .HasColumnType("int")
                        .HasColumnName("BrojSobe");

                    b.Property<int?>("HotelID")
                        .HasColumnType("int");

                    b.Property<int>("KonacnaCena")
                        .HasColumnType("int")
                        .HasColumnName("KonacnaCena");

                    b.Property<bool>("PlacenRacun")
                        .HasColumnType("bit")
                        .HasColumnName("PlacenRacun");

                    b.Property<bool>("RoomService")
                        .HasColumnType("bit")
                        .HasColumnName("RoomService");

                    b.Property<bool>("SpaCentar")
                        .HasColumnType("bit")
                        .HasColumnName("SpaCentar");

                    b.Property<string>("TipSobe")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TipSobe");

                    b.HasKey("ID");

                    b.HasIndex("HotelID");

                    b.ToTable("Racun");
                });

            modelBuilder.Entity("Server.Models.Soba", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<int>("BrojDana")
                        .HasColumnType("int")
                        .HasColumnName("BrojDana");

                    b.Property<int>("BrojKreveta")
                        .HasColumnType("int")
                        .HasColumnName("BrojKreveta");

                    b.Property<int>("BrojSobe")
                        .HasColumnType("int")
                        .HasColumnName("BrojSobe");

                    b.Property<string>("Gost")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Gost");

                    b.Property<int?>("HotelID")
                        .HasColumnType("int");

                    b.Property<bool>("Sredjivanje")
                        .HasColumnType("bit")
                        .HasColumnName("Sredjivanje");

                    b.Property<int>("TrenutniKapacitet")
                        .HasColumnType("int")
                        .HasColumnName("TrenutniKapacitet");

                    b.Property<bool>("Zauzeta")
                        .HasColumnType("bit")
                        .HasColumnName("Zauzeta");

                    b.HasKey("ID");

                    b.HasIndex("HotelID");

                    b.ToTable("Soba");
                });

            modelBuilder.Entity("Server.Models.Racun", b =>
                {
                    b.HasOne("Server.Models.Hotel", "Hotel")
                        .WithMany("Racuni")
                        .HasForeignKey("HotelID");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Server.Models.Soba", b =>
                {
                    b.HasOne("Server.Models.Hotel", "Hotel")
                        .WithMany("Sobe")
                        .HasForeignKey("HotelID");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Server.Models.Hotel", b =>
                {
                    b.Navigation("Racuni");

                    b.Navigation("Sobe");
                });
#pragma warning restore 612, 618
        }
    }
}
