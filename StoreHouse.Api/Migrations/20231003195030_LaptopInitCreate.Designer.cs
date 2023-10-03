﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StoreHouse.Database.StoreHouseDbContext;

#nullable disable

namespace StoreHouse.Api.Migrations
{
    [DbContext(typeof(StoreHouseContext))]
    [Migration("20231003195030_LaptopInitCreate")]
    partial class LaptopInitCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StoreHouse.Database.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BankCard")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Dish", (string)null);
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.DishesCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DishesCategories");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PrimeCost")
                        .HasColumnType("numeric");

                    b.Property<double>("Remains")
                        .HasColumnType("double precision");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Ingredient", (string)null);
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.IngredientsCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IngredientsCategories");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("ImageId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PrimeCost")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.ProductList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Count")
                        .HasColumnType("double precision");

                    b.Property<int>("DishId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PrimeCost")
                        .HasColumnType("numeric");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("integer");

                    b.Property<int>("SemiProductId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.Property<int>("SupplyId")
                        .HasColumnType("integer");

                    b.Property<int>("WriteOffId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DishId");

                    b.HasIndex("ReceiptId");

                    b.HasIndex("SemiProductId");

                    b.HasIndex("SupplyId");

                    b.HasIndex("WriteOffId");

                    b.ToTable("ProductList", (string)null);
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClientId")
                        .HasColumnType("integer");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CloseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OpenDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("UserId");

                    b.ToTable("Receipt", (string)null);
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.SemiProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Output")
                        .HasColumnType("double precision");

                    b.Property<string>("Prescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PrimeCost")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("SemiProducts");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Supply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.HasIndex("UserId");

                    b.ToTable("Supply", (string)null);
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedLogin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PinCode")
                        .HasColumnType("integer");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.WriteOff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CauseId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CauseId");

                    b.HasIndex("UserId");

                    b.ToTable("WriteOff", (string)null);
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.WriteOffCause", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WriteOffCauses");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Dish", b =>
                {
                    b.HasOne("StoreHouse.Database.Entities.DishesCategory", "Category")
                        .WithMany("Dishes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Ingredient", b =>
                {
                    b.HasOne("StoreHouse.Database.Entities.IngredientsCategory", "Category")
                        .WithMany("Ingredients")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Product", b =>
                {
                    b.HasOne("StoreHouse.Database.Entities.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.ProductList", b =>
                {
                    b.HasOne("StoreHouse.Database.Entities.Dish", "Dish")
                        .WithMany("ProductLists")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreHouse.Database.Entities.Receipt", "Receipt")
                        .WithMany("ProductLists")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreHouse.Database.Entities.SemiProduct", "SemiProduct")
                        .WithMany("ProductLists")
                        .HasForeignKey("SemiProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreHouse.Database.Entities.Supply", "Supply")
                        .WithMany("ProductLists")
                        .HasForeignKey("SupplyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreHouse.Database.Entities.WriteOff", "WriteOff")
                        .WithMany("ProductLists")
                        .HasForeignKey("WriteOffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Receipt");

                    b.Navigation("SemiProduct");

                    b.Navigation("Supply");

                    b.Navigation("WriteOff");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Receipt", b =>
                {
                    b.HasOne("StoreHouse.Database.Entities.Client", "Client")
                        .WithMany("Receipts")
                        .HasForeignKey("ClientId");

                    b.HasOne("StoreHouse.Database.Entities.User", "User")
                        .WithMany("Receipts")
                        .HasForeignKey("UserId");

                    b.Navigation("Client");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Supply", b =>
                {
                    b.HasOne("StoreHouse.Database.Entities.Supplier", "Supplier")
                        .WithMany("Supplies")
                        .HasForeignKey("SupplierId");

                    b.HasOne("StoreHouse.Database.Entities.User", "User")
                        .WithMany("Supplies")
                        .HasForeignKey("UserId");

                    b.Navigation("Supplier");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.User", b =>
                {
                    b.HasOne("StoreHouse.Database.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.WriteOff", b =>
                {
                    b.HasOne("StoreHouse.Database.Entities.WriteOffCause", "Cause")
                        .WithMany("WriteOffs")
                        .HasForeignKey("CauseId");

                    b.HasOne("StoreHouse.Database.Entities.User", "User")
                        .WithMany("WriteOffs")
                        .HasForeignKey("UserId");

                    b.Navigation("Cause");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Client", b =>
                {
                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Dish", b =>
                {
                    b.Navigation("ProductLists");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.DishesCategory", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.IngredientsCategory", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Receipt", b =>
                {
                    b.Navigation("ProductLists");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.SemiProduct", b =>
                {
                    b.Navigation("ProductLists");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Supplier", b =>
                {
                    b.Navigation("Supplies");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.Supply", b =>
                {
                    b.Navigation("ProductLists");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.User", b =>
                {
                    b.Navigation("Receipts");

                    b.Navigation("Supplies");

                    b.Navigation("WriteOffs");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.WriteOff", b =>
                {
                    b.Navigation("ProductLists");
                });

            modelBuilder.Entity("StoreHouse.Database.Entities.WriteOffCause", b =>
                {
                    b.Navigation("WriteOffs");
                });
#pragma warning restore 612, 618
        }
    }
}
