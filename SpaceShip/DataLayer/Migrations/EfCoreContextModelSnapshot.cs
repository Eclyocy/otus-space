﻿// <auto-generated />
using System;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(EfCoreContext))]
    partial class EfCoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Spaceship.DataLayer.EfClasses.Problem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ResourceTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("Spaceship.DataLayer.EfClasses.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("ResourceTypeId")
                        .HasColumnType("integer");

                    b.Property<Guid>("SpaceshipId")
                        .HasColumnType("uuid");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SpaceshipId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Spaceship.DataLayer.EfClasses.ResourceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ProblemId")
                        .HasColumnType("integer");

                    b.Property<int?>("ResourceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.HasIndex("ResourceId");

                    b.ToTable("ResourcesType");
                });

            modelBuilder.Entity("Spaceship.DataLayer.EfClasses.Spaceship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<int>("ThisDay")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Spaceships");
                });

            modelBuilder.Entity("Spaceship.DataLayer.EfClasses.Resource", b =>
                {
                    b.HasOne("Spaceship.DataLayer.EfClasses.Spaceship", "Spaceship")
                        .WithMany()
                        .HasForeignKey("SpaceshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Spaceship");
                });

            modelBuilder.Entity("Spaceship.DataLayer.EfClasses.ResourceType", b =>
                {
                    b.HasOne("Spaceship.DataLayer.EfClasses.Problem", null)
                        .WithMany("ResourcesType")
                        .HasForeignKey("ProblemId");

                    b.HasOne("Spaceship.DataLayer.EfClasses.Resource", null)
                        .WithMany("ResourcesType")
                        .HasForeignKey("ResourceId");
                });

            modelBuilder.Entity("Spaceship.DataLayer.EfClasses.Problem", b =>
                {
                    b.Navigation("ResourcesType");
                });

            modelBuilder.Entity("Spaceship.DataLayer.EfClasses.Resource", b =>
                {
                    b.Navigation("ResourcesType");
                });
#pragma warning restore 612, 618
        }
    }
}
