﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrgChartApi.Models;

namespace OrgChartApi.Migrations.Calendar
{
    [DbContext(typeof(CalendarContext))]
    [Migration("20210708163112_AddCalendar")]
    partial class AddCalendar
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("Calendar", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Calendar");
                });

            modelBuilder.Entity("CalendarEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CalendarId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CalendarId");

                    b.ToTable("CalendarEvent");
                });

            modelBuilder.Entity("CalendarEvent", b =>
                {
                    b.HasOne("Calendar", "Calendar")
                        .WithMany("CalendarEvent")
                        .HasForeignKey("CalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calendar");
                });

            modelBuilder.Entity("Calendar", b =>
                {
                    b.Navigation("CalendarEvent");
                });
#pragma warning restore 612, 618
        }
    }
}
