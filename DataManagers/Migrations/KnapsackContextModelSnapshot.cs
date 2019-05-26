﻿// <auto-generated />
using System;
using DataManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataManagers.Migrations
{
    [DbContext(typeof(KnapsackContext))]
    partial class KnapsackContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("DataManagers.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("KnapsackId");

                    b.Property<int?>("KnapsackTaskId");

                    b.Property<int>("Value");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("KnapsackId");

                    b.HasIndex("KnapsackTaskId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("DataManagers.Knapsack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity");

                    b.HasKey("Id");

                    b.ToTable("Knapsacks");
                });

            modelBuilder.Entity("DataManagers.KnapsackTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("KnapsackId");

                    b.Property<int>("MaxValue");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Percent");

                    b.Property<int?>("SolveId");

                    b.Property<bool>("Status");

                    b.Property<int>("Time");

                    b.HasKey("Id");

                    b.HasIndex("KnapsackId")
                        .IsUnique();

                    b.HasIndex("SolveId")
                        .IsUnique();

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("DataManagers.Solve", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("K");

                    b.Property<string>("Table");

                    b.Property<int>("W");

                    b.HasKey("Id");

                    b.ToTable("Solves");
                });

            modelBuilder.Entity("DataManagers.Item", b =>
                {
                    b.HasOne("DataManagers.Knapsack", "Knapsack")
                        .WithMany("Items")
                        .HasForeignKey("KnapsackId");

                    b.HasOne("DataManagers.KnapsackTask", "KnapsackTask")
                        .WithMany("Items")
                        .HasForeignKey("KnapsackTaskId");
                });

            modelBuilder.Entity("DataManagers.KnapsackTask", b =>
                {
                    b.HasOne("DataManagers.Knapsack", "Knapsack")
                        .WithOne("KnapsackTask")
                        .HasForeignKey("DataManagers.KnapsackTask", "KnapsackId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataManagers.Solve", "Solve")
                        .WithOne("KnapsackTask")
                        .HasForeignKey("DataManagers.KnapsackTask", "SolveId");
                });
#pragma warning restore 612, 618
        }
    }
}