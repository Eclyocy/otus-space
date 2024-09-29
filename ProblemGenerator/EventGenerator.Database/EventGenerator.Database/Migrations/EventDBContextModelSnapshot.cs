﻿// <auto-generated />
using System;
using EventGenerator.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventGenerator.Database.Migrations
{
    [DbContext(typeof(EventDBContext))]
    partial class EventDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EventGenerator.Database.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("EventLevel")
                        .HasColumnType("integer")
                        .HasColumnName("event_level");

                    b.Property<Guid>("GeneratorId")
                        .HasColumnType("uuid")
                        .HasColumnName("generator_id");

                    b.HasKey("Id");

                    b.ToTable("event");
                });

            modelBuilder.Entity("EventGenerator.Database.Models.Generator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ShipId")
                        .HasColumnType("uuid")
                        .HasColumnName("ship_id");

                    b.Property<int>("TroubleCoins")
                        .HasColumnType("integer")
                        .HasColumnName("trouble_coins");

                    b.HasKey("Id");

                    b.ToTable("generator");
                });
#pragma warning restore 612, 618
        }
    }
}