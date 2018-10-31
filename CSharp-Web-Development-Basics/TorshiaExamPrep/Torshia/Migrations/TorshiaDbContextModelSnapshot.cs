﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Torshia.Data;

namespace Torshia.Migrations
{
    [DbContext(typeof(TorshiaDbContext))]
    partial class TorshiaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Torshia.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ReportedOn");

                    b.Property<int>("ReporterId");

                    b.Property<int>("Status");

                    b.Property<int>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("ReporterId");

                    b.HasIndex("TaskId")
                        .IsUnique();

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Torshia.Models.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("Torshia.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime?>("DueDate");

                    b.Property<bool>("IsReported");

                    b.Property<string>("Participants");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Torshia.Models.TaskSector", b =>
                {
                    b.Property<int>("SectorId");

                    b.Property<int>("TaskId");

                    b.HasKey("SectorId", "TaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskSectors");
                });

            modelBuilder.Entity("Torshia.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Torshia.Models.Report", b =>
                {
                    b.HasOne("Torshia.Models.User", "Reporter")
                        .WithMany("Reports")
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Torshia.Models.Task", "Task")
                        .WithOne("Report")
                        .HasForeignKey("Torshia.Models.Report", "TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Torshia.Models.TaskSector", b =>
                {
                    b.HasOne("Torshia.Models.Sector", "Sector")
                        .WithMany()
                        .HasForeignKey("SectorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Torshia.Models.Task", "Task")
                        .WithMany("AffectedSectors")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
