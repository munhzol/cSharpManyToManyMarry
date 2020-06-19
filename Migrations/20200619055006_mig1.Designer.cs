﻿// <auto-generated />
using System;
using LoginRegTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoginRegTest.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200619055006_mig1")]
    partial class mig1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LoginRegTest.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("cshLoginRegTest.Models.Plan", b =>
                {
                    b.Property<int>("PlanID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserID");

                    b.Property<string>("Wedder1")
                        .IsRequired();

                    b.Property<string>("Wedder2")
                        .IsRequired();

                    b.HasKey("PlanID");

                    b.HasIndex("UserID");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("cshLoginRegTest.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PlanID");

                    b.Property<int>("UserID");

                    b.HasKey("ReservationID");

                    b.HasIndex("PlanID");

                    b.HasIndex("UserID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("cshLoginRegTest.Models.Plan", b =>
                {
                    b.HasOne("LoginRegTest.Models.User", "Organizer")
                        .WithMany("MyPlans")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("cshLoginRegTest.Models.Reservation", b =>
                {
                    b.HasOne("cshLoginRegTest.Models.Plan", "MyPlan")
                        .WithMany("Guests")
                        .HasForeignKey("PlanID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LoginRegTest.Models.User", "Guest")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
