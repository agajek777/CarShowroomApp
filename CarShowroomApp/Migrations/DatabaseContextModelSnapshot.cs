﻿// <auto-generated />
using System;
using CarShowroomApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarShowroomApp.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarShowroomApp.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Engine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Mileage")
                        .HasColumnType("float");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Power")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("Production")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Audi",
                            Description = "Autko w super stanie! Garażowane! Od fanatyka!",
                            Engine = "V10 5.2",
                            ImagePath = "https://apollo-ireland.akamaized.net/v1/files/eyJmbiI6IjV1Z3FkOTBqbzBqNzEtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.RWMwGPIAdM3kRNxqXNsJhlDtpAK4npaZ2LUk4TheVJE/image;s=1080x720;cars_;/937787999_;slot=10;filename=eyJmbiI6IjV1Z3FkOTBqbzBqNzEtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.RWMwGPIAdM3kRNxqXNsJhlDtpAK4npaZ2LUk4TheVJE_rev001.jpg",
                            Mileage = 130000.0,
                            Model = "RS6",
                            Power = 580,
                            Price = 120000.0,
                            Production = new DateTime(2012, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Audi",
                            Description = "Niestety koszty utrzymania samochodu to fanaberia!",
                            Engine = "V10 5.2",
                            ImagePath = "http://blog.ozonee.pl/wp-content/uploads/2018/07/Audi-R8-V10.jpg",
                            Mileage = 54000.0,
                            Model = "R8",
                            Power = 525,
                            Price = 320000.0,
                            Production = new DateTime(2010, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
