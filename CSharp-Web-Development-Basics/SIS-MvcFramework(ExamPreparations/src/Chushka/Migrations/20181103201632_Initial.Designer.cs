﻿// <auto-generated />
using System;
using Chushka.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chushka.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181103201632_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Chushka.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<DateTime>("OrderedOn");

                    b.Property<string>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProductId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Chushka.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Chushka.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Chushka.Models.Order", b =>
                {
                    b.HasOne("Chushka.Models.User", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("Chushka.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
