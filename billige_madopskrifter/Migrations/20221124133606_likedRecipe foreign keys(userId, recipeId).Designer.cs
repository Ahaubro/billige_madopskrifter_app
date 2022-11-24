﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using billige_madopskrifter.Data;

#nullable disable

namespace billige_madopskrifter.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20221124133606_likedRecipe foreign keys(userId, recipeId)")]
    partial class likedRecipeforeignkeysuserIdrecipeId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("billige_madopskrifter.Model.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Alergene")
                        .HasColumnType("longtext");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<string>("MeasurementUnit")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("billige_madopskrifter.Model.LikedRecipes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("LikedRecipes");
                });

            modelBuilder.Entity("billige_madopskrifter.Model.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<double>("EstimatedPrice")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NumberOfPersons")
                        .HasColumnType("int");

                    b.Property<int>("PrepTime")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("billige_madopskrifter.Model.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("billige_madopskrifter.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("billige_madopskrifter.Model.Ingredient", b =>
                {
                    b.HasOne("billige_madopskrifter.Model.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("billige_madopskrifter.Model.LikedRecipes", b =>
                {
                    b.HasOne("billige_madopskrifter.Model.Recipe", null)
                        .WithMany("LikedRecipes")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("billige_madopskrifter.Model.User", null)
                        .WithMany("LikedRecipes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("billige_madopskrifter.Model.Recipe", b =>
                {
                    b.HasOne("billige_madopskrifter.Model.User", null)
                        .WithMany("Recipes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("billige_madopskrifter.Model.Review", b =>
                {
                    b.HasOne("billige_madopskrifter.Model.Recipe", null)
                        .WithMany("Reviews")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("billige_madopskrifter.Model.User", null)
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("billige_madopskrifter.Model.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("LikedRecipes");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("billige_madopskrifter.Model.User", b =>
                {
                    b.Navigation("LikedRecipes");

                    b.Navigation("Recipes");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
