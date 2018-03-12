﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using waveRiderTester.Data;

namespace waveRiderTester.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("waveRiderTester.Models.Buoy", b =>
                {
                    b.Property<int>("BuoyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Latitude")
                        .IsRequired();

                    b.Property<string>("Longtitude")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NbdcId")
                        .IsRequired();

                    b.Property<string>("Owner")
                        .IsRequired();

                    b.HasKey("BuoyId");

                    b.ToTable("Buoy");
                });
#pragma warning restore 612, 618
        }
    }
}
