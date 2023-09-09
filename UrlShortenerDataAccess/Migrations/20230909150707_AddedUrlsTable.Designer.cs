﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrlShortener.DataAccess;

namespace UrlShortenerDataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230909150707_AddedUrlsTable")]
    partial class AddedUrlsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UrlShortener.ObjectModel.UriModels.BaseUrl", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OriginalURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShortenedURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OriginalURL")
                        .IsUnique();

                    b.HasIndex("ShortenedURL")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("ResourceLocators");
                });

            modelBuilder.Entity("UrlShortener.ObjectModel.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SaltedHashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UrlShortener.ObjectModel.UriModels.BaseUrl", b =>
                {
                    b.HasOne("UrlShortener.ObjectModel.User", "User")
                        .WithMany("BaseURLs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UrlShortener.ObjectModel.User", b =>
                {
                    b.Navigation("BaseURLs");
                });
#pragma warning restore 612, 618
        }
    }
}
