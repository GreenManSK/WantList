﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WantList.Data;

namespace WantList.Data.Migrations
{
    [DbContext(typeof(WantListDbContext))]
    [Migration("20210122154306_DeletedField")]
    partial class DeletedField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WantList.Core.AnidbAnime", b =>
                {
                    b.Property<int>("AnidbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("English")
                        .HasColumnType("text");

                    b.Property<string>("Japanese")
                        .HasColumnType("text");

                    b.HasKey("AnidbId");

                    b.ToTable("AnidbAnimes");
                });

            modelBuilder.Entity("WantList.Core.Anime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddedDateTime")
                        .HasColumnType("datetime");

                    b.Property<int?>("AnidbId")
                        .HasColumnType("int");

                    b.Property<bool>("BluRay")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("BluRayRelease")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("EpisodeCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Quality")
                        .HasColumnType("int");

                    b.Property<bool>("Redownload")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("WantRank")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnidbId")
                        .IsUnique();

                    b.ToTable("Animes");
                });

            modelBuilder.Entity("WantList.Core.Manga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddedDateTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("Completed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MangaUpdatesId")
                        .HasColumnType("int");

                    b.Property<string>("MissingVolumes")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("WantRank")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MangaUpdatesId")
                        .IsUnique();

                    b.ToTable("Mangas");
                });

            modelBuilder.Entity("WantList.Core.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AnidbLastSync")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });
#pragma warning restore 612, 618
        }
    }
}
