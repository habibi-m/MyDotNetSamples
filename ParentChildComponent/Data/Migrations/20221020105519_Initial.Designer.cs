﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParentChildComponent.Data;

#nullable disable

namespace ParentChildComponent.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221020105519_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ParentChildComponent.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "category1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "category2",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "category3",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 4,
                            Name = "category4"
                        },
                        new
                        {
                            Id = 5,
                            Name = "category5",
                            ParentId = 4
                        },
                        new
                        {
                            Id = 6,
                            Name = "category6"
                        },
                        new
                        {
                            Id = 7,
                            Name = "category7",
                            ParentId = 6
                        },
                        new
                        {
                            Id = 8,
                            Name = "category8",
                            ParentId = 7
                        },
                        new
                        {
                            Id = 9,
                            Name = "category9",
                            ParentId = 6
                        },
                        new
                        {
                            Id = 10,
                            Name = "category10",
                            ParentId = 9
                        },
                        new
                        {
                            Id = 11,
                            Name = "category11",
                            ParentId = 10
                        },
                        new
                        {
                            Id = 12,
                            Name = "category12",
                            ParentId = 11
                        });
                });

            modelBuilder.Entity("ParentChildComponent.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Name = "product1",
                            Price = 100,
                            RegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Stock = 0
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Name = "product2",
                            Price = 200,
                            RegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Stock = 0
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Name = "product3",
                            Price = 300,
                            RegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Stock = 0
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 4,
                            Name = "product4",
                            Price = 400,
                            RegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Stock = 0
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 8,
                            Name = "product5",
                            Price = 500,
                            RegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Stock = 0
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 8,
                            Name = "product6",
                            Price = 600,
                            RegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Stock = 0
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 10,
                            Name = "product7",
                            Price = 700,
                            RegisterDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Stock = 0
                        });
                });

            modelBuilder.Entity("ParentChildComponent.Models.Category", b =>
                {
                    b.HasOne("ParentChildComponent.Models.Category", "Parent")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("ParentChildComponent.Models.Product", b =>
                {
                    b.HasOne("ParentChildComponent.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ParentChildComponent.Models.Category", b =>
                {
                    b.Navigation("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
